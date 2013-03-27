using System;

namespace Kibus
{
	public class Fuente
	{
		private IntPtr apuntadorFuente;
		
		public Fuente(string archivo, short tamaño)
	    {
	    	apuntadorFuente = SDL.CargarFuente(archivo, tamaño);
	    }
	      
	    public  IntPtr LeerPuntero()
	    {
	    	return apuntadorFuente;
	    }
	}
}

