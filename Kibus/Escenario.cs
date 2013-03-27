using System;
using System.Collections.Generic;
using Tao.Sdl;

namespace Kibus
{
	public class Escenario
	{
		private Nivel nivel;
		private Kibus kibus;
		bool continuar;
		
		private Stack<int> pasos;
		
		public Escenario()
		{
			nivel = new Nivel();
		}
		
		public void Iniciar()
		{
			pasos = new Stack<int>();
			continuar = true;
			
			nivel.PosicionarCasa();
			
			kibus = new Kibus(nivel.GetCasa().GetX(), nivel.GetCasa().GetY());
			
			
			//usaurio parte
			while(continuar)
			{
				Dibujar();
				ComprobarTeclas();
				MoverElementos();
				SDL.Pausar(30);
			}
			
			
			//IA parte
			while(pasos.Count > 0)
			{
				Dibujar();
				SDL.EscribirTexto("Volviendo a Casa", 10, 10); 
				SDL.RefrescarPantalla();
				Regresar();
				MoverElementos();
				SDL.Pausar(300);
			}
			
			Dibujar();
			SDL.EscribirTexto("KIBUS LLEGO! \\O/",(short)(SDL.alto/2), (short)(SDL.ancho/2));
			SDL.RefrescarPantalla();
			while(!SDL.TeclaPulsada(Sdl.SDLK_RETURN))
			{
				SDL.Pausar(20);
			}
		}
		
		private void Dibujar()
		{
			SDL.DibujarFondo();
			nivel.Dibujar();
			kibus.Dibujar();
			SDL.RefrescarPantalla();
		}
		
		private void ComprobarTeclas()
		{
			
			if(SDL.TeclaPulsada(Sdl.SDLK_RETURN))
			{
				continuar = false;
			}

	        if ((SDL.TeclaPulsada(Sdl.SDLK_UP))
	            && nivel.EsPosibleMover(kibus.GetX(), (short)(kibus.GetY() - kibus.GetVelocidadY()),
	                    kibus.GetXFinal(), (short)(kibus.GetYFinal() - kibus.GetVelocidadY())))
			{
				//Console.WriteLine("Arriba");
				pasos.Push(Sdl.SDLK_DOWN);
	            kibus.MoverArriba();
			}
	
	        if ((SDL.TeclaPulsada(Sdl.SDLK_DOWN))
	            && nivel.EsPosibleMover(kibus.GetX(),(short)(kibus.GetY() + kibus.GetVelocidadY()),
	                   kibus.GetXFinal(), (short)(kibus.GetYFinal() + kibus.GetVelocidadY())))
				
			{
				//Console.WriteLine("Abajo");
				pasos.Push(Sdl.SDLK_UP);
	            kibus.MoverAbajo();
			}
	
	        if ((SDL.TeclaPulsada(Sdl.SDLK_RIGHT))
	            && nivel.EsPosibleMover((short)(kibus.GetX() + kibus.GetVelocidadX()), kibus.GetY(),
	                   (short)(kibus.GetXFinal() + kibus.GetVelocidadX()), kibus.GetYFinal()))
			{
				//Console.WriteLine("Derecha");
				pasos.Push(Sdl.SDLK_LEFT);
	            kibus.MoverDerecha();
			}
	
	        if ((SDL.TeclaPulsada(Sdl.SDLK_LEFT) )
	            && nivel.EsPosibleMover((short)(kibus.GetX() - kibus.GetVelocidadX()), kibus.GetY(),
	                   (short)(kibus.GetXFinal() - kibus.GetVelocidadX()), kibus.GetYFinal()))
			{
				//Console.WriteLine("Izquierda");
				pasos.Push(Sdl.SDLK_RIGHT);
	            kibus.MoverIquierda();
			}

		}
		
		private void MoverElementos()
		{
			kibus.Dibujar();
		}
		
		private void Regresar()
		{
			switch(pasos.Pop())
			{
				case Sdl.SDLK_UP:
					kibus.MoverArriba();
					break;
				case Sdl.SDLK_DOWN:
					kibus.MoverAbajo();
					break;
				case Sdl.SDLK_LEFT:
					kibus.MoverIquierda();
					break;
				case Sdl.SDLK_RIGHT:
					kibus.MoverDerecha();
					break;
			}
		}
		
		
	}
}
