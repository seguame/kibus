//  
//  Abeja.cs
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
using System.Collections.Generic;
using Utileria;


namespace Graficos
{
	public class Abeja : Personaje
	{
		public Queue<Direccion> Propagacion
		{
			private set;
			get;
		}
		
		public Abeja (short x, short y) : base ("Assets/GFX/Bee.png", x ,y)
		{
			Propagacion = new Queue<Direccion>();
		}

		public void Mover(Direccion direccion, int temperatura)
		{
			base.Mover(direccion);
			Propagacion.Enqueue(direccion);
			TemperaturaAlcanzada = temperatura;
		}

		public int TemperaturaAlcanzada
		{
			get;
			private set;
		}
		
		public void reiniciarPropagacion()
		{
			Propagacion.Clear();
		}
	}
}

