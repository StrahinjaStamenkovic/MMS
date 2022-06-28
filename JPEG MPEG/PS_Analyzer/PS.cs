
using System;
using System.IO;


namespace PS_analyzer
{


	public class PS
	{ // Clasa pentru un Program Stream (MPEG 2)

//--------------- Variabile membre -----------------------------------

		// Fisierul asociat
		FileStream file;				// fisierul MPEG

		// Buffer de date
		public byte[] buf = new byte[2097152];	// Buffer de 2 Mb
		public int	buf_len;					// Nr. de bytes in buffer
		public int pos;							// Byte-ul curent

		// Pentru operatii din clasa bit
		public bit b = new bit();

//--------------------------------------------------------------------


//--------------- Functii membre -------------------------------------

		// Constructorul implicit
		public PS()
		{
			buf_len=0;	// Setare lungime buffer
			pos=0;		// Setare byte curent
		}

		// Constructor cu initializare fisier
		public PS(string nume_fisier)
		{ 
			file = new FileStream(nume_fisier,FileMode.Open);	// Deschide fisierul sursa
			buf_len=0;											// Setare lungime buffer
			pos=0;												// Setare byte curent
		}

		// Citeste din fisier in buffer cat se specifica -> refill
		public void fill_buffer(int st)
		{
			buf_len=file.Read(buf,st,2097152-st)+st;				// Citeste din fisier
			pos=0;													// Actualizeaza pozitia
		}
		
		// Verifica daca in buffer mai este suficienta informatie.Daca nu face refill.
		public void check_buffer(int need)
		{ int i;
			if ((buf_len-pos<need)&&(buf_len==2097152))			// Daca e nevoie de informatie
			{
				for(i=pos;i<buf_len;i++) buf[i-pos]=buf[i];		// Copiaza ce nu s-a folosit inca
				fill_buffer(buf_len-pos);						// la inceputul buffer-ului
			}
		}

		// Citeste 4 bytes si decide ce tip de pachet urmeaza. Returneaza :
		// MPEG_program_end_code	( 185 = 441(0x1B9) - 256 )
		// PACK_Header				( 186 = 442(0x1BA) - 256 )
		// SYSTEM_HEADER			( 187 = 443(0x1BB) - 256 )
		// PES_PACKET				( Stream_ID(0xBC,...)	 )
		// UNKNOWN					(           0			 )
		public byte read_code()
		{ 
			check_buffer(4);
		 // verifica daca primii 3 bytes = 0x01H
			if (b.ret_value(buf,pos,pos+2)!=1) return 0;// returneza UNKNOWN
			pos=pos+4;									// deplasare pozitie curenta
			return buf[pos-1];							// valoare returnata - ultimul byte
		}

		// Deplaseaza pozitia curenta in buffer cu un nr. de bytes
		public void skip_info(int n_bytes)
		{
			check_buffer(n_bytes);
			pos=pos+n_bytes;
		}

		// Returneaza valoarea aflata intr-un nr. de bytes de la pozitia curenta
		public int ret_value(int n_bytes)
		{
			int val;
			check_buffer(n_bytes);
			val = b.ret_value(buf,pos,pos+n_bytes-1);
			pos=pos+n_bytes;
			return val;
		}

		// Returneaza valoarea aflata intr-un nr. de biti din byte-ul curent
		public int ret_bit_value(int st_bit,int end_bit,bool act)
		{
			int val;
			val=b.ret_bit_value(buf[pos],st_bit,end_bit);
			if(act) pos++;
			return val;
		}

		// Inchiderea fisierului asociat
		public void end_PS()
		{
			file.Close();
		}

		public bool zero() // Returneaza true dc. pe pozitia curenta se afla 0
		{
			if (buf[pos]==0) return true;
			return false;
		}

		public void go_back(int step) // Derulare inapoi
		{
			pos=pos-step;
		}

		public double get_SCR() // Valoarea SCR din PACK_HEADER
		{
			int SCR;
			double scr_val;

			SCR=ret_bit_value(2,4,false);		// SCR(32..30)
			scr_val=(SCR>3)?4294967296.0:0.0;	// caz pt. SCR[32]=1 -> overflow
			SCR=SCR<<30;
			SCR|=ret_bit_value(6,7,true)<<28;	// SCR(29,28)
			SCR|=ret_value(1)<<20;				// SCR(27..20)
			SCR|=ret_bit_value(0,4,false)<<15;	// SCR(19..15)
			SCR|=ret_bit_value(6,7,true)<<13;	// SCR(14,13)
			SCR|=ret_value(1)<<5;				// SCR(12..5)
			SCR|=ret_bit_value(0,4,false);		// SCR(4..0)
			scr_val+=Convert.ToDouble(SCR);
			scr_val*=300.0;
			SCR=ret_bit_value(6,7,true)<<7;		// SCR_ext(8,7)
			SCR|=ret_bit_value(0,6,true);		// SCR_ext(6..0)
			scr_val+=Convert.ToDouble(SCR);
			return scr_val/27000.0;				// 27Mhz clock
		}

		public double get_PES_stamp()	// Obtine PTS-ul sau DTS-ul din PES-uri
		{
			int stamp;
			double val;

			stamp=ret_bit_value(4,6,true);			// PTS(32..30)
			val=(stamp>3)?4294967296.0:0.0;			// caz de overflow (PTS[32]=1)
			stamp=stamp<<30;
			stamp|=ret_value(1)<<22;				// PTS(29..22)
			stamp|=ret_bit_value(0,6,true)<<15;		// PTS(21..15)
			stamp|=ret_value(1)<<7;					// PTS(14..7)
			stamp|=ret_bit_value(0,6,true);			// PTS(6..0)
			val+=Convert.ToDouble(stamp);
			return val/90.0;						// 90Khz clock
		}
					
		public bool ret_bit(int pz)
		{
		return b.ret_bit(buf[pos],pz);
		}

//--------------------------------------------------------------------		
		
	}
}
