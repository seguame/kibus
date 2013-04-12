//  
//  Nivel.cs
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
using Tao.Sdl;

using Graficos;
using Esedelish;

namespace Niveles
{
	internal abstract class Nivel
	{
		protected Sprite[,] sprites;
		protected Sprite casa;
		protected Kibus kibus;
		
		protected Nivel()
		{
			sprites = new Sprite[10,10];
		}
		
		public bool EsPosibleMover(short x, short y, short xFin, short yFin)
		{
			if(xFin < Hardware.ancho + 64 - 201 && yFin < Hardware.alto + 64 && xFin > 0 && yFin > 0)
			{
				foreach(Sprite sprite in sprites)
				{
					if(sprite != null &&  sprite != casa && sprite.ColisionCon(x, y, xFin, yFin))
					{
						return false;
					}
				}
				
				return true;
			}
			
			return false;
		}
		
		protected void DibujarTodo()
		{
			Hardware.DibujarFondo();
			
			DibujarObstaculos();
			kibus.Dibujar();
			casa.Dibujar();
			
			Hardware.RefrescarPantalla();
			
		}
		
		protected void DibujarObstaculos()
		{
			foreach(Sprite sprite in sprites)
			{
				if(sprite != null)
				{
					sprite.Dibujar();
				}
			}
		}
		
		public Sprite GetCasa()
		{
			return casa;
		}
		
		protected void MoverElementos()
		{
			kibus.Dibujar();
		}
		
		public abstract void Iniciar();
	}
}

