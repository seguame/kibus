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

		public int OnTabaX
		{
			get
			{
				int incremento = 0;
				
				if(ultimoMovimiento != Direccion.MISINGNO)
				{
					switch(ultimoMovimiento)
					{
						case Direccion.ARRIBA_DER: 
						case Direccion.ABAJO_DER: 
						case Direccion.DERECHA: 
							incremento = -1; 
							break;
							
						case Direccion.IZQUIERDA: 
						case Direccion.ABAJO_IZQ: 
						case Direccion.ARRIBA_IZQ: 
							incremento = 1;
							break;
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
						case Direccion.ARRIBA_DER: 
						case Direccion.ARRIBA_IZQ: 
						case Direccion.ARRIBA: 
							incremento = -1;  
							break;
						
						case Direccion.ABAJO: 
						case Direccion.ABAJO_IZQ:
						case Direccion.ABAJO_DER: 
							incremento = 1; 
							break;
					}
				}
				
				return OnToyY + incremento;
			}
		}
		public Kibus (short x, short y) :base ("Assets/GFX/personaje.png")
		{
			X = x;
			Y = y;
		}
		
		public Kibus() : base ("Assets/GFX/personaje.png")
		{
			X = 0;
			Y = 0;
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
	        Y -= incremento_Y;
	    }
	
	    private void MoverAbajo()
	    {
	        Y += incremento_Y;
	    }
	
	    private  void MoverDerecha()
	    {
	        X += incremento_X;
	    }

	    private  void MoverIzquierda()
	    {
	        X -= incremento_X;
	    }
		
		private void MoverArriba_Derecha()
		{
			Y -= incremento_Y;
			X += incremento_X;
		}
		
		private void MoverArriba_Izquierda()
		{
			Y -= incremento_Y;
			X -= incremento_X;
		}
		
		private void MoverAbajo_Derecha()
		{
			Y += incremento_Y;
			X += incremento_X;
		}
		
		private void MoverAbajo_Izquierda()
		{
			Y += incremento_Y;
			X -= incremento_X;
		}
	}
}

