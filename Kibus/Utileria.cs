//  
//  Utileria.cs
//  
//  Author:
//       seguame <>
// 
//  Copyright (c) 2013 seguame
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
		ABAJO_DER
	};
	
	public static class Extencion
	{
		public static string ToString(this Direccion direccion)
		{
			switch(direccion)
			{
			case Direccion.ABAJO:
				return "Abajo";
				break;
			case Direccion.ABAJO_DER:
				return "Abajo-Derecha";
				break;
			case Direccion.ABAJO_IZQ:
				return "Abajo-Izquierda";
				break;
			case Direccion.ARRIBA:
				return "Arriba";
				break;
			case Direccion.ARRIBA_DER:
				return "Arriba-Derecha";
				break;
			case Direccion.ARRIBA_IZQ:
				return "Arriba-Izquierda";
				break;
			case Direccion.DERECHA:
				return "Derecha";
				break;
			case Direccion.IZQUIERDA:
				return "Izquierda";
				break;
			}
		}
	}
}

