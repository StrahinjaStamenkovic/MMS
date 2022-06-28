using System;

namespace PS_analyzer
{
	public class bit
	{// Clasa de acces la nivel de bit a elementelor dintr-un byte

		public byte set_bit(byte x,int pos)
		{// Seteaza bitul de pe pozitia pos din x la valoarea 1
			int r;
			r=ret_bit_value(x,0,pos-1);
			r=r<<1;
			r+=1;
			r=r<<7-pos;
			r+=ret_bit_value(x,pos+1,7);
			return Convert.ToByte(r);
		}

		public byte clr_bit(byte x,int pos)
		{// Seteaza bitul de pe pozitia pos din x la valoarea 0
			int r;
			r=ret_bit_value(x,0,pos-1);
			r=r<<7-pos+1;
			r+=ret_bit_value(x,pos+1,7);
			return Convert.ToByte(r);
		}

		public bool ret_bit(byte x,int pos)
		{// Afla daca un bit din x este setat(1) sau nu(0)
			int r;
			if ((pos<0)||(pos>7)) return false;
			r=Convert.ToInt32(x)>>(7-pos);
			if(r%2==1) return true;
			return false;
		}

		public int ret_value(byte[] data,int st_byte,int end_byte)
		{// Returneaza valoarea in baza 10 dintr-un nr. de bytes
			int i,ret;
			ret=0;
			for (i=st_byte;i<=end_byte;i++)
			{
				ret=ret<<8;
				ret+=data[i];
			}
			return ret;
		}

		public int ret_bit_value(byte x,int st_bit,int end_bit)
		{// Returneaza valoarea in baza 10 dintr-un nr. de biti dintr-un byte
			int i,val;
			if(end_bit<st_bit) return 0;
			val=0;
			for(i=st_bit;i<=end_bit;i++)
			{
				val=val<<1;
				if (ret_bit(x,i)) val+=1;
			}
			return val;
		}

	}
}
