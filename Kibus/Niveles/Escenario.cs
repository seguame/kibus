﻿//  
//  Escenario.cs
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

namespace Niveles
{
	public class Escenario
	{
		
		private Nivel nivel;
		
		public Escenario(Menu.Opcion opcion)
		{
			switch(opcion)
			{
				case Menu.Opcion.PRACTICA_1:
					nivel = new Nivel1();
					break;
				
				case Menu.Opcion.PRACTICA_2:
					nivel = new Nivel2();
					break;
				
				case Menu.Opcion.PRACTICA_3:
					nivel = new Nivel3();
					break;
				
				case Menu.Opcion.PRACTICA_4:
					nivel = new Nivel4();
					break;
				
				case Menu.Opcion.CREAR_NIVEL:
					nivel = new EditorMapas();
					break;
			}
		}
		
		public void Iniciar()
		{
			nivel.Iniciar();
		}
	}
}
