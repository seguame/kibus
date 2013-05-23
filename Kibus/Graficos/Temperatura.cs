//
//  Temperatura.cs
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

namespace Graficos
{
	public class Temperatura
	{
		private const uint MASK = 0xFFFFFF;
		public Sdl.SDL_Rect Rectangulo;
		public uint RGB{ private set; get;}

		public int Calor
		{
			get; set;
		}

		public Temperatura (int x, int y, int calor)
		{
			Rectangulo = new Sdl.SDL_Rect((short)(x * 32), (short)(y * 32), 32, 32);

			Calor = calor;

			CambiarColor();
		}

		public void ActualizarTemperatura()
		{
			CambiarColor();
		}

		private void CambiarColor()
		{
			uint r = (byte)(Calor > 255 ? 255 : Calor);
			uint g = (byte)(Calor > 255 ? Calor % 255 : 0);
			uint b = (byte)(Calor > 510 ? Calor % 255 : 0);
			
			RGB = ( MASK & r) << 16 | (MASK & g) << 8 | (MASK & b);
		}
	}
}

