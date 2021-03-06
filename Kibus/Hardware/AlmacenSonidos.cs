//  
//  AlmacenSonidos.cs
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
using SdlDotNet.Audio;

namespace Esedelish
{
	public class AlmacenSonidos
	{
		public static Music MusicaFondo
		{
			private set;
			get;
		}
		
		internal static void Inicializar()
		{
			if(!Hardware.SonidoActivo)
				return;
			
			MusicaFondo = new Music ("Assets/Sonidos/choco1.ogg");
			MusicPlayer.Volume = 30;
			MusicPlayer.Load ( MusicaFondo );
			MusicPlayer.Play(true);
		}
	}
}

