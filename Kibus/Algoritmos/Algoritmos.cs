//  
//  Algoritmos.cs
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
using System.Collections.Generic;

using Utileria;


namespace Algoritmos
{
	public class Algoritmos
	{
		/*
		 * void lineaBres(int xa, int ya, int xb, int yb)
			{
				int x, y, dx, dy, dezplazamientoX, dezplazamientoY, p;
				
				dx= abs(xb-xa);
				dy= abs(yb-ya);
				x= xa;
				y= ya;
				
				if(xa>xb)
				{
					dezplazamientoX= -1;
				}
				else
				{
					dezplazamientoX= 1;
				}
				
				if(ya>yb)
				{
					dezplazamientoY= -1;
				}
				else
				{
					dezplazamientoY= 1;
				}
				
				if(control_seleccionado == BORRADOR)
				{
					borrador(xa, ya);
				}
				else
				{
					putPixel2(xa,ya,xb,yb);
				}
				
				if(dx>=dy)
				{
					p= dx/2;
					while(x!=xb)
					{
						p= p-dy;
						if(p<0)
						{
							y= y+dezplazamientoY;
							p= p+dx;
						}
						x= x+dezplazamientoX;
						if(control_seleccionado == BORRADOR)
						{
							borrador(xa, ya);
						}
						else
						{
							putPixel2(xa,ya,xb,yb);
						}
					}
				}
				else if(dy>dx)
				{
					p= dy/2;
					while (y!=yb)
					{
						p= p-dx;
						if(p<0)
						{
							x= x+dezplazamientoX;
							p= p+dy;
						}
						y= y+dezplazamientoY;
						if(control_seleccionado == BORRADOR)
						{
							borrador(xa, ya);
						}
						else
						{
							putPixel2(xa,ya,xb,yb);
						}
					}
				}
				glFlush();
			}*/
		
		public static Queue<Direccion> CalculaLineaBresenham(int Xinicial, int Yinicial, int Xfinal, int Yfinal)
		{
			Console.WriteLine("{0} {1} {2} {3}", Xinicial, Yinicial, Xfinal, Yfinal);
			int deltaX, deltaY, x, y, ultimaX, ultimaY, p;
			int incrementoY = 1,incrementoX = 1; //el incremento es positivo
			
			deltaX = Math.Abs(Xinicial - Xfinal);
			deltaY = Math.Abs(Yinicial - Yfinal);
			
			if(deltaY > deltaX) //m >= 1
		    {
				p =  (2 * deltaX) - deltaY;
				
				if(Yinicial > Yfinal)
				{
					//cambiamos los puntos
					x       = Xfinal;
					y       = Yfinal;
					ultimaY = Yinicial;//Y final ahora es Y inicial
					
					if(Xinicial < Xfinal)//si la coord X inicial es menor que la final
		            {
						//tenemos que irnos hacia atras
						incrementoX = -1;
					}
				}
				else
				{
					//los puntos se quedan normales
					x       = Xinicial;
					y       = Yinicial;
					ultimaY = Yfinal;
				}
				
				//Console.WriteLine("Pondriamos X:{0} Y:{1}", x, y);
				//ponerLineas(x, y, x, y);
				
				while(y < ultimaY) //mientras Y no sea Y final
		        {
					y += incrementoY; //vamos incrementando Y
					
					if (p < 0) //si 'p' es negativa, solo incrementamos Y
		            {
						p += 2 * deltaX;
						Console.WriteLine("Pondriamos X:{0} Y:{1}", x, y - incrementoY);
						//ponerLineas(x, y - incrementoY, x, y);
					}
					else //sino, incrementamos X
		            {
						x += incrementoX;
						p += 2 *(deltaX - deltaY);
						
						Console.WriteLine("Pondriamos X:{0} Y:{1}", x - incrementoX, y - incrementoY);
						//ponerLineas(x - incrementoX, y - incrementoY, x, y);
					}
				}
			}
			else  //0 <= m < 1
		    {
				p = (2 * deltaY) - deltaX;
				
				if(Xinicial > Xfinal)
				{
					//cambiamos los puntos
					x       = Xfinal;
					y       = Yfinal;
					ultimaX = Xinicial;
					
					
					if(Yinicial < Yfinal)
					{
						//tenemos que graficar hacia abajo
						incrementoY = -1;
					}
				}
				else
				{
					//los puntos se quedan normales
					x       = Xinicial;
					y       = Yinicial;
					ultimaX = Xfinal;
				}
				
				//Console.WriteLine("Pondriamos X:{0} Y:{1}", x, y);
				//ponerLineas(x, y, x, y);
				
				while(x < ultimaX)
				{
					x += incrementoX;
					
					if (p < 0)//si 'p' es negativo solo incrementamos X
		            {
						p += 2 * deltaY;
						Console.WriteLine("Pondriamos X:{0} Y:{1}", x - incrementoX, y);
						//ponerLineas(x - incrementoX, y, x, y);
					}
					else
					{
						//sino incrementamos Y
						y += incrementoY;
						p += 2 * (deltaY - deltaX);
						
						Console.WriteLine("Pondriamos X:{0} Y:{1}", x - incrementoX, y - incrementoY);
						//ponerLineas(x - incrementoX, y - incrementoY, x, y);
					}
				}
			}
			
			return null;
		}
	}
}

