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
		private Queue<Direccion> propagacion;
		private Stack<Direccion> retropropagacion;
		
		public Abeja (short x, short y) : base ("Assets/GFX/Bee.png", x ,y)
		{
			retropropagacion = new Stack<Direccion>();
		}

		public void Mover(Direccion direccion, int temperatura)
		{
			//Reiniciar la cola de movimientos local que se puede copiar
			if(propagacion.Count != 0)
			{
				propagacion.Clear();
			}

			base.Mover(direccion);
			retropropagacion.Push(direccion);
			TemperaturaAlcanzada = temperatura;
		}

		public int TemperaturaAlcanzada
		{
			get;
			private set;
		}


		public Queue<Direccion> CopiarTrayectoria()
		{
			if(propagacion == null)
			{
				propagacion = new Queue<Direccion>();
			}

			while(retropropagacion.Count != 0)
			{
				propagacion.Enqueue(retropropagacion.Pop());
			}

			return propagacion;
		}
	}
}

