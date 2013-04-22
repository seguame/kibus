//  
//  Sprite.cs
//  
//  Author:
//       Miguel Seguame Reyes <seguame@outlook.com>
// 
//  Copyright (c) 2013 Miguel Seguame Reyes
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Runtime.InteropServices;
using Tao.Sdl;

using Esedelish;

namespace Graficos
{
	public class Sprite
	{
	    protected short xInicial;
	    protected short yInicial;
	    protected short incremento_X;
	    protected short incremento_Y;
	    protected Imagen[] secuencia;
	    protected byte fotogramaActual;
	    protected bool animado = false;
		protected bool visible = true;
	    protected Sdl.SDL_Rect rectangulo;

		public Imagen ImagenActual
		{
			private set;
			get;
		}

		public IntPtr ApuntadorImagen
		{
			get { return ImagenActual.ApuntadorImagen; }
		}

		public bool Visible
		{
			set { visible = value; }
			get { return visible; }
		}
		
		public bool Bandera
		{
			set;
			get;
		}

		public short X
		{
			get { return rectangulo.x; }
			protected set 
			{ 
				rectangulo.x = value;

				if(rectangulo.x < 0) rectangulo.x = 0;
				if(rectangulo.x > Hardware.Ancho) rectangulo.x = Hardware.Ancho;
			}
		}

		public short Y
		{
			get { return rectangulo.y; }

			protected set 
			{ 
				rectangulo.y = value; 
				if(rectangulo.y < 0) rectangulo.y = 0;
				if(rectangulo.y > Hardware.Alto) rectangulo.y = Hardware.Alto;
			}
		}

		public short Alto
		{
			protected set { rectangulo.h = value; }
			get { return rectangulo.h; }
		}

		public short Ancho
		{
			protected set { rectangulo.w = value; }
			get { return rectangulo.w; }
		}

		public int OnToyX
		{
			get { return X / Ancho; }
		}
		
		public int OnToyY
		{
			get { return Y / Alto; }
		}
		
		public Sprite (string archivo)
		{
			ImagenActual = new Imagen(archivo);
			Sdl.SDL_Surface superficie = (Sdl.SDL_Surface)Marshal.PtrToStructure(ImagenActual.ApuntadorImagen, typeof(Sdl.SDL_Surface));

			
			fotogramaActual = 0;
			animado = false;
			Alto = (short)superficie.h;
			Ancho = Alto; // se supone que son graficos proporcion 1:1
			incremento_X = Ancho;
			incremento_Y = Alto;
			
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
			
			ImagenActual = secuencia[fotogramaActual];
		}
		
		public void Dibujar()
		{
			if(Visible)
			{
				Hardware.DibujarSprite(this);
			}
		}
		
		public bool ColisionCon(Sprite otro)
		{
			return ((X + Ancho > otro.X) 
			   		&& (X < otro.X + otro.Ancho) 
			   		&& (Y + Alto > otro.Y) 
			        && (Y < otro.Y + otro.Alto));
		}
		
		public bool ColisionCon(int nx, int ny, int nxmax, int nymax)
	    {
	        return ((nxmax > X)
	            	&& (nymax > Y)
	            	&& (X + Ancho > nx)
			        && (Y + Alto > ny));
	    }

		
		public Sdl.SDL_Rect GetRectangulo()
		{
			return rectangulo;
		}
		
		public void Mover(int x, int y)
		{
			X = (short)x;
			Y = (short)y;
		}
		
		public void Mover(Sdl.SDL_Rect rect)
		{
			X = rect.x;
			Y = rect.y;
		}
		
		public short GetVelocidadX()
		{
			return incremento_X;
		}
		
		public short GetVelocidadY()
		{
			return incremento_Y;
		}
		
		public short GetXFinal()
	    {
	        return (short)(X + Ancho);
	    }
	
	    public short GetYFinal()
	    {
	        return (short)(Y + Alto);
	    }
		
	}
}

