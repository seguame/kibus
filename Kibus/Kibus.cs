using System;

namespace Kibus
{
	public class Kibus: Sprite
	{
		public Kibus (short x, short y) :base ("GFX/personaje.png")
		{
			rectangulo.x = x;
			rectangulo.y = y;
		}
		
		public void MoverArriba()
	    {
	        rectangulo.y -= incremento_Y;
	    }
	
	    public void MoverAbajo()
	    {
	        rectangulo.y += incremento_Y;
	    }
	
	    public  void MoverDerecha()
	    {
	        rectangulo.x += incremento_X;
	    }

	    public  void MoverIquierda()
	    {
	        rectangulo.x -= incremento_X;
	    }
	}
}

