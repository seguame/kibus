//  
//  Servicio_SDL.cs
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

namespace Esedelish
{
	public class Hardware
	{
		private static IntPtr pantalla;
		private static Sdl.SDL_Color color; 
		private static Sdl.SDL_Rect origen;
		private static Fuente fuente;
		private static short ancho;
		private static short alto;
		
		public static bool SonidoActivo
		{
			private set;
		 	get;
		}
		
		public static short BarraHerramientas
		{
			private set;
			get;
		}
		
		public static short Ancho
		{
			private set { ancho = value ;}
			get { return (short)(ancho - BarraHerramientas);}
		}
		
		public static short Alto
		{
			private set { alto = value ;}
			get { return alto;}
		}
		
		public static void Inicializar(short ancho, short alto, int colores,bool pantalla_completa)
		{
			BarraHerramientas = 200;
			
			int bpp;
			int flags = Sdl.SDL_HWSURFACE | Sdl.SDL_DOUBLEBUF | Sdl.SDL_ANYFORMAT;
			
			Console.WriteLine("Inicializando SDL");
			
			Hardware.Ancho = ancho;
			Hardware.Alto = alto;
			
			if(pantalla_completa)
			{
				flags |= Sdl.SDL_FULLSCREEN;
			}
			
			#region video
			
			if(Sdl.SDL_Init(Sdl.SDL_INIT_VIDEO) < 0)
			{
				Console.WriteLine("No se pudo inicar el sistema de video");
				Console.WriteLine("ERROR: {0}", Sdl.SDL_GetError());
				Environment.Exit(1);
				
			}
			
			bpp = Sdl.SDL_VideoModeOK(ancho, alto, colores, flags);
			
			if(bpp == 0)
			{
				Console.WriteLine("No se pudo establecer el modo de video");
				Console.WriteLine("ERROR: {0}", Sdl.SDL_GetError());
				Environment.Exit(1);
			}
			
			
			
			pantalla = Sdl.SDL_SetVideoMode(ancho, alto, bpp, flags);
			#endregion
			
			#region Fuentes
			
			if(SdlTtf.TTF_Init() < 0)
			{
				Console.WriteLine("No se pudo inicar el manejador de fuentes");
				Console.WriteLine("ERROR: {0}", Sdl.SDL_GetError());
				Environment.Exit(1);
			}
			
			#endregion
			
			origen = new Sdl.SDL_Rect(0 , 0, ancho, alto);
			//Sdl.SDL_SetClipRect(pantalla, ref origen);
			
			#region Sonido
			
			if(Sdl.SDL_InitSubSystem(Sdl.SDL_INIT_AUDIO) < 0)
			{
				Console.WriteLine("No se pudo inicar el sistema de audio");
				Console.WriteLine("ERROR: {0}", Sdl.SDL_GetError());
				SonidoActivo = false;
			}
			else
			{
				SonidoActivo = true;
			}
			
			if(SonidoActivo)
			{
				if(SdlMixer.Mix_OpenAudio(22050, Sdl.AUDIO_S16, 2, 4096) < 0)
				{
					Console.WriteLine("No se pudo inicar el mezclador de audio");
					Console.WriteLine("ERROR: {0}", Sdl.SDL_GetError());
					SonidoActivo = false;
				}
			}
			
			AlmacenSonidos.Inicializar();
			
			#endregion
			
			Console.WriteLine("SDL inicializado con exito");
			
			Sprite boton = new Sprite("Assets/Botones/boton.png");
			
			fuente = new Fuente("Assets/Fuentes/FreeSansBold.ttf", 14);
			CambiarColorTexto(0,0,0);
			
			boton.Mover(650, 100);
			boton.Dibujar();
			EscribirTexto("Arriba", 685, 115);
			
			boton.Mover(650, 154);
			boton.Dibujar();
			EscribirTexto("Abajo", 685, 169);
			
			boton.Mover(650, 207);
			boton.Dibujar();
			EscribirTexto("Izquierda", 685, 222);
			
			boton.Mover(650, 270);
			boton.Dibujar();
			EscribirTexto("Derecha", 685, 285);
			
			boton.Mover(650, 324);
			boton.Dibujar();
			EscribirTexto("Buscar", 685, 339);
			
			
			fuente = new Fuente("Assets/Fuentes/FreeSansBold.ttf", 20);
		}
		
		public static void Pausar(uint tiempo)
		{
			Sdl.SDL_Delay(tiempo);
		}
		
		public static void RefrescarPantalla()
		{
			Sdl.SDL_Flip(pantalla);
		}
		
		public static bool TeclaPulsada(int c)
		{
			bool pulsada = false;
			Sdl.SDL_Event evento;
			
			Sdl.SDL_PollEvent(out evento);
			
			int numkeys;
		    byte[] teclas = Tao.Sdl.Sdl.SDL_GetKeyState(out numkeys);
			
		    if (teclas[c] == 1)
			{
		        pulsada = true;
			}

			//Sdl.SDL_PumpEvents();

			switch(evento.type)
			{
				case Sdl.SDL_MOUSEBUTTONDOWN:
					if(evento.button.x > 650 
				   		&& evento.button.x < 783)
					{
						if(evento.button.y > 100
				   			&& evento.button.y < 153
							&& c == Sdl.SDLK_UP)
						{
							pulsada = true;
						}
						else if(evento.button.y > 154
					   		&& evento.button.y < 206
					        && c == Sdl.SDLK_DOWN)
						{
							pulsada = true;
						}
						else if(evento.button.y > 207
					   		&& evento.button.y < 269
					        && c == Sdl.SDLK_LEFT)
						{
							pulsada = true;
						}
						else if(evento.button.y > 270
					   		&& evento.button.y < 323
					        && c == Sdl.SDLK_RIGHT)
						{
							pulsada = true;
						}
						else if(evento.button.y > 324
						   		&& evento.button.y < 379
						        && c == Sdl.SDLK_RETURN)
						{
							pulsada = true;
						}

					Console.WriteLine("X: {0} Y:{1}", evento.button.x, evento.button.y);
					}
					
					break;
			}
			
			return pulsada;
		}
		
		public static void CambiarColorTexto(byte r, byte g, byte b)
		{
			color = new Sdl.SDL_Color(r,g,b);
		}
		
		
		#region Carga de Elementos
		
		public static IntPtr CargarFuente(string archivo, short tamaño)
		{
			IntPtr fuente = SdlTtf.TTF_OpenFont(archivo, tamaño);
			
			if(fuente == IntPtr.Zero)
			{
				Console.WriteLine("Fuente inexistente: {0}", archivo);
				Environment.Exit(1);
			}
			
			return fuente;
		}
		
		public static IntPtr CargarImagen(string archivo)
		{
			IntPtr imagen;
			
			imagen = SdlImage.IMG_Load(archivo);
			
			if(imagen == IntPtr.Zero)
			{
				Console.WriteLine("Imagen inexistente: {0}", archivo);
				Environment.Exit(1);
			}
			
			return imagen;
		}
		
		#endregion
		
		#region Dibujado de Elementos
		
		public static void DibujarImagen(IntPtr imagen, short x, short y)
		{
			Sdl.SDL_Rect destino = new Sdl.SDL_Rect(x,y, Ancho, Alto);
			Sdl.SDL_BlitSurface(imagen,ref origen, pantalla, ref destino);
		}
		
		public static void DibujarSprite(Sprite sprite)
		{
			Sdl.SDL_Rect destino = sprite.GetRectangulo();
			IntPtr imagen = sprite.ApuntadorImagen;
			
			Sdl.SDL_BlitSurface(imagen, ref origen, pantalla, ref destino);
		}
		
		public static void EscribirTexto(string texto, int x, int y)
		{
			if(Object.ReferenceEquals(color, null))
			{
				color = new Sdl.SDL_Color(255, 255, 255);
			}
			
			IntPtr textoComoImagen = SdlTtf.TTF_RenderText_Solid(fuente.ApuntadorFuente, texto, color);
			
			if(textoComoImagen == IntPtr.Zero)
			{
				Console.WriteLine("Error al renderizar '{0}'", texto);
			}

			Sdl.SDL_Rect destino = new Sdl.SDL_Rect((short)x,(short)y,Ancho , Alto);
			
			Sdl.SDL_BlitSurface(textoComoImagen, ref origen, pantalla, ref destino);
		}
		
		public static void DibujarFondo(byte r, byte g, byte b)
		{
			SdlGfx.boxRGBA(pantalla, 0, 0, Ancho, Alto, r, g, b, 0xFF); 
		}
		
		public static void DibujarCuadroColor(Sprite sprite, byte r, byte g, byte b)
		{
			SdlGfx.boxRGBA(pantalla, sprite.X, sprite.Y, sprite.Ancho, sprite.Alto, r, g, b, 0xFF); 
		}
		
		
		public static void DibujarFondo()
		{
			Sprite pasto = new Sprite("Assets/GFX/pasto.jpg");
			
			for(int i = 0; i < 20; i++)
			{
				for(int j = 0; j < 20; j++)
				{
					pasto.Mover(i * pasto.Ancho, j * pasto.Alto);
					pasto.Dibujar();
				}
			}
		}
		
		#endregion
		
	}
}
