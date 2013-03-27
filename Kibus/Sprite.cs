using System;
using System.Runtime.InteropServices;
using Tao.Sdl;

namespace Kibus
{
	public class Sprite
	{
	    //protected short x;
	    //protected short y;
	    protected short xInicial;
	    protected short yInicial;
	    protected short incremento_X;
	    protected short incremento_Y;
	    protected Imagen imagen;
	    protected Imagen[] secuencia;
	    protected byte fotogramaActual;
	    protected bool animado = false;
	    protected Sdl.SDL_Rect rectangulo;
		
		
		public Sprite (string archivo)
		{
			imagen = new Imagen(archivo);
			//Sdl.SDL_Surface superficie = (Sdl.SDL_Surface)Marshal.PtrToStructure(imagen.PunteroImagen(), typeof(Sdl.SDL_Surface));
			
			//Console.WriteLine(superficie.h);
			
			fotogramaActual = 0;
			animado = false;
			rectangulo.h = 64;//(short)superficie.h;
			rectangulo.w = 64;//(short)superficie.;
			incremento_X = 64;//superficie.w;
			incremento_Y = 64;//superficie.h;
			
		}
		
		public Sprite(string[] archivos) : this(archivos[0])
		{
			byte tamaño = (byte)archivos.Length;
			byte i = 0;
			secuencia = new Imagen[tamaño];
			
			animado = true;
			foreach(string archivo in archivos)
			{
				secuencia[i++] = new Imagen(archivo);
			}
		}
		
		public void SiguienteCuadro()
		{
			if(!animado)
			{
				return;
			}
			
			if(fotogramaActual < secuencia.Length - 1)
			{
				fotogramaActual++;
			}
			else
			{
				fotogramaActual = 0;
			}
			
			imagen = secuencia[fotogramaActual];
		}
		
		public void Dibujar()
		{
			SDL.DibujarSprite(this);
		}
		
		public bool ColisionCon(Sprite otro)
		{
			if((rectangulo.x + GetAncho() > otro.GetX()) 
			   && (rectangulo.x < otro.GetX() + otro.GetAncho()) 
			   && (rectangulo.y + GetAlto() > otro.GetY()) 
			   && (rectangulo.y < otro.GetY() + otro.GetAlto()))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public bool ColisionCon(int nx, int ny, int nxmax, int nymax)
	    {
	        if ((nxmax > rectangulo.x)
	            && (nymax > rectangulo.y)
	            && (rectangulo.x + GetAncho() > nx)
	            && (rectangulo.y + GetAlto() > ny))
			{
	            return true;
			}
	        else
			{
	            return false;
			}
	    }
		
		public IntPtr GetImagen()
		{
			return imagen.PunteroImagen();
		}
		
		public Sdl.SDL_Rect GetRectangulo()
		{
			return rectangulo;
		}
		
		public void Mover(int x, int y)
		{
			rectangulo.x = (short)x;
			rectangulo.y = (short)y;
		}
		
		public void Mover(Sdl.SDL_Rect rect)
		{
			rectangulo.x = rect.x;
			rectangulo.y = rect.y;
		}
		
		public short GetX()
		{
			return rectangulo.x;
		}
		
		public short GetY()
		{
			return rectangulo.y;
		}
		
		public short GetVelocidadX()
		{
			return incremento_X;
		}
		
		public short GetVelocidadY()
		{
			return incremento_Y;
		}
		
		public short GetAncho()
		{
			return rectangulo.w;
		}
		
		public short GetAlto()
		{
			return rectangulo.h;
		}
		
		public short GetXFinal()
	    {
	        return (short)(rectangulo.x+GetAncho());
	    }
	
	    public short GetYFinal()
	    {
	        return (short)(rectangulo.y+GetAlto());
	    }
		
	}
}

