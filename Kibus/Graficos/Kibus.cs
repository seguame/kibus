//  
//  Kibus.cs
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
using Utileria;

namespace Graficos
{
	public class Kibus: Sprite
	{
		
		private Direccion ultimoMovimiento = Direccion.MISINGNO;
		
		public int OnToyX
		{
			get
			{
				return rectangulo.x / 64;
			}
		}
		
		public int OnToyY
		{
			get
			{
				return rectangulo.y / 64;
			}
		}
		
		public int OnTabaX
		{
			get
			{
				int incremento = 0;
				
				if(ultimoMovimiento != Direccion.MISINGNO)
				{
					switch(ultimoMovimiento)
					{
						case Direccion.ARRIBA_DER: incremento = -1; break;
						case Direccion.ABAJO_DER: incremento = -1; break;
						case Direccion.DERECHA: incremento = -1; break;
							
						case Direccion.IZQUIERDA: incremento = 1; break;
						case Direccion.ABAJO_IZQ: incremento = 1; break;
						case Direccion.ARRIBA_IZQ: incremento = 1; break;
					}
				}
				
				return OnToyX + incremento;
			}
		}
		
		public int OnTabaY
		{
			get
			{
				int incremento = 0;
				
				if(ultimoMovimiento != Direccion.MISINGNO)
				{
					switch(ultimoMovimiento)
					{
						case Direccion.ARRIBA_DER: incremento = -1; break;
						case Direccion.ARRIBA_IZQ: incremento = -1; break;
						case Direccion.ARRIBA: incremento = -1;  break;
						
						case Direccion.ABAJO: incremento = 1; break;
						case Direccion.ABAJO_IZQ: incremento = 1; break;
						case Direccion.ABAJO_DER: incremento = 1; break;
					}
				}
				
				return OnToyY + incremento;
			}
		}
		public Kibus (short x, short y) :base ("Assets/GFX/personaje.png")
		{
			rectangulo.x = x;
			rectangulo.y = y;
			
		}
		
		public Kibus() : base ("Assets/GFX/personaje.png")
		{
			rectangulo.x = 0;
			rectangulo.y = 0;
			
		}
		
		public void Mover(Direccion direccion)
		{
			switch(direccion)
			{
				case Direccion.ARRIBA:
					MoverArriba();
					break;
				case Direccion.ABAJO:
					MoverAbajo();
					break;
				case Direccion.ABAJO_DER:
					MoverAbajo_Derecha();
					break;
				case Direccion.ABAJO_IZQ:
					MoverAbajo_Izquierda();
					break;
				case Direccion.ARRIBA_DER:
					MoverArriba_Derecha();
					break;
				case Direccion.ARRIBA_IZQ:
					MoverArriba_Izquierda();
					break;
				case Direccion.DERECHA:
					MoverDerecha();
					break;
				case Direccion.IZQUIERDA:
					MoverIzquierda();
					break;
				default:
					throw new NotImplementedException("Movimiendo no definido");
			}
		}
		
		private void MoverArriba()
	    {
	        rectangulo.y -= incremento_Y;
			
			if(rectangulo.y < 0) rectangulo.y = 0;
	    }
	
	    private void MoverAbajo()
	    {
	        rectangulo.y += incremento_Y;
			if(rectangulo.y > 640) rectangulo.y = 640;
	    }
	
	    private  void MoverDerecha()
	    {
	        rectangulo.x += incremento_X;
			if(rectangulo.x > 640) rectangulo.x = 640;
	    }

	    private  void MoverIzquierda()
	    {
	        rectangulo.x -= incremento_X;
			if(rectangulo.x < 0) rectangulo.x = 0;
	    }
		
		private void MoverArriba_Derecha()
		{
			rectangulo.y -= incremento_Y;
			rectangulo.x += incremento_X;
			
			if(rectangulo.y < 0) rectangulo.y = 0;
			if(rectangulo.x > 640) rectangulo.x = 640;
		}
		
		private void MoverArriba_Izquierda()
		{
			rectangulo.y -= incremento_Y;
			rectangulo.x -= incremento_X;
			
			if(rectangulo.y < 0) rectangulo.y = 0;
			if(rectangulo.x < 0) rectangulo.x = 0;
		}
		
		private void MoverAbajo_Derecha()
		{
			rectangulo.y += incremento_Y;
			rectangulo.x += incremento_X;
			
			if(rectangulo.y > 640) rectangulo.y = 640;
			if(rectangulo.x > 640) rectangulo.x = 640;
		}
		
		private void MoverAbajo_Izquierda()
		{
			rectangulo.y += incremento_Y;
			rectangulo.x -= incremento_X;
			
			if(rectangulo.y > 640) rectangulo.y = 640;
			if(rectangulo.x < 0) rectangulo.x = 0;
		}
	}
}

