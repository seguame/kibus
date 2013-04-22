//  
//  Utileria.cs
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


namespace Utileria
{
	public enum Direccion
	{
		ARRIBA,
		ABAJO,
		IZQUIERDA,
		DERECHA,
		ARRIBA_IZQ,
		ARRIBA_DER,
		ABAJO_IZQ,
		ABAJO_DER,
		MISINGNO
	};
	
	public static class DireccionUtils
	{
		public static Direccion DeterminarDireccion(int nuevaX, int nuevaY, int viejaX, int viejaY,bool xInversa, bool yInversa)
		{
			//Primero las direcciones compuestas
			if(viejaX > nuevaX && viejaY < nuevaY){ Console.WriteLine("vX:{0} > nX:{2} | vY:{1} < nY:{3}  {4} {5}", viejaX, viejaY, nuevaX, nuevaY, xInversa, yInversa); return Direccion.ABAJO_IZQ;}
			if(viejaX < nuevaX && viejaY < nuevaY){ Console.WriteLine("vX:{0} < nX:{2} | vY:{1} < nY:{3}  {4} {5}", viejaX, viejaY, nuevaX, nuevaY, xInversa, yInversa); return Direccion.ABAJO_DER;}
			if(viejaX > nuevaX && viejaY > nuevaY){ Console.WriteLine("vX:{0} > nX:{2} | vY:{1} > nY:{3}  {4} {5}", viejaX, viejaY, nuevaX, nuevaY, xInversa, yInversa); return Direccion.ARRIBA_IZQ;}
			if(viejaX < nuevaX && viejaY > nuevaY){ Console.WriteLine("vX:{0} < nX:{2} | vY:{1} > nY:{3}  {4} {5}", viejaX, viejaY, nuevaX, nuevaY, xInversa, yInversa); return Direccion.ARRIBA_DER;}
			
			//Y ya pues las normales
			if(viejaX < nuevaX){ Console.WriteLine("vX:{0} < nX:{1} {2}", viejaX, nuevaX, xInversa); return Direccion.DERECHA; }
			if(viejaX > nuevaX){ Console.WriteLine("vX:{0} > nX:{1} {2}", viejaX, nuevaX, xInversa); return Direccion.IZQUIERDA; }
			if(viejaY < nuevaY){ Console.WriteLine("vY:{0} < nY:{1} {2}", viejaY, nuevaY, yInversa); return Direccion.ABAJO; }
			if(viejaY > nuevaY){ Console.WriteLine("vX:{0} > nX:{1} {2}", viejaY, nuevaY, yInversa); return Direccion.ARRIBA; }
			
			return Direccion.MISINGNO;
		}
	}
	
	/*
	 * private void testingManual()
		{
			Direccion dir = Direccion.MISINGNO;
			
			while(true)
			{
				DibujarTodo();
				if(Hardware.TeclaPulsada(Sdl.SDLK_RETURN))
				{
					break;
				}
	
		        if (Hardware.TeclaPulsada(Sdl.SDLK_UP))
				{
					dir = Direccion.ARRIBA;
				}
		
		        if (Hardware.TeclaPulsada(Sdl.SDLK_DOWN))
				{
					dir = Direccion.ABAJO;
				}
				
				if (Hardware.TeclaPulsada(Sdl.SDLK_RIGHT))
				{
					dir = Direccion.DERECHA;
				}
		
		        if (Hardware.TeclaPulsada(Sdl.SDLK_LEFT))
				{
					dir = Direccion.IZQUIERDA;
				}
				
				if (Hardware.TeclaPulsada(Sdl.SDLK_q))
				{
					dir = Direccion.ARRIBA_IZQ;
				}
		
		        if (Hardware.TeclaPulsada(Sdl.SDLK_e))
				{
					dir = Direccion.ARRIBA_DER;
				}
				
				if (Hardware.TeclaPulsada(Sdl.SDLK_a))
				{
					dir = Direccion.ABAJO_IZQ;
				}
		
		        if (Hardware.TeclaPulsada(Sdl.SDLK_d))
				{
					dir = Direccion.ABAJO_DER;
				}
		
		        if(IntentarMover(dir))
				{
					Console.WriteLine(dir);
					kibus.Mover(dir);
					dir = Direccion.MISINGNO;
				}
				
				MoverElementos();
				Hardware.Pausar(100);
			}
		}*/
	
}

