using System;
using SdlDotNet.Audio;

namespace Kibus
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
			if(!SDL.SonidoActivo)
				return;
			
			MusicaFondo = new Music ("Sound/choco1.ogg");
			MusicPlayer.Volume = 30;
			MusicPlayer.Load ( MusicaFondo );
			MusicPlayer.Play(true);
		}
	}
}

