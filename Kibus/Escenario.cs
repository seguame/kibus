using System;
using Tao.Sdl;

namespace Kibus
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
				case Menu.Opcion.CREAR_NIVEL:
					nivel = new EditorNivel();
					break;
			}
		}
		
		public void Iniciar()
		{
			nivel.Iniciar();
		}
	}
}
