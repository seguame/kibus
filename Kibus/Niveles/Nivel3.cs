//  
//  Nivel3.cs
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
using Graficos;
using System.Collections.Generic;
using Utileria;
using Esedelish;
using Algoritmos;
using Tao.Sdl;


namespace Niveles
{
	internal class Nivel3 : Nivel
	{
		private const int ABEJAS = 5;
		private const int RITUALES = 5;
		private Abeja[] abejinis;
		private int[][] temperatura;
		private int[][] mapa;
		private List<Temperatura> temperaturas;
		private bool colorearTemperatura;
		
		public Nivel3 ()
		{
			abejinis = new Abeja[ABEJAS];
			new CargadorMapas().SeleccionarArchivo();
		}
		
		public override void Iniciar()
		{
			mapa = CargadorMapas.Mapa;
			
			//TODO:
			//Quitar de todos el tamaño fijo
			for(int i = 0; i < 20; i++)
			{
				for(int j = 0; j < 20; j++)
				{
					if(mapa[i][j] == EditorMapas.CASA)
					{
						Console.WriteLine("Casa en {0},{1}", i, j);
						this.casa = sprites[i,j] = new Sprite("Assets/GFX/casini.png");
						requierePosicionarCasa = false;
					}
					else if(mapa[i][j] != 0)
					{
						Console.WriteLine("{2} en {0},{1}", i, j, mapa[i][j]);
						sprites[i,j] = new Sprite("Assets/GFX/"+mapa[i][j]+".png");
						mapa[i][j] = -1;
					}
					
					if(sprites[i,j] != null)
					{
						sprites[i,j].Mover((short)(i * sprites[i,j].Ancho),(short) (j * sprites[i,j].Alto));
					}
				}
			}
			
			PosicionarKibus();
			
			CalentarCeldas();
			
			BuscarCasa();
		}
		
		private void BuscarCasa()
		{
			Random random = new Random(System.DateTime.Now.Millisecond);
			Direccion direccion;
			Abeja elegible;
			bool casaEncontrada = false;
			
			//Inicializar abejas
			for(int i = 0; i < ABEJAS; i++)
			{
				abejinis[i] = new Abeja(kibus.X, kibus.Y);
			}
			
			elegible = new Abeja(0,0);
			elegible.Visible = false;
			
			do
			{
				colorearTemperatura = Hardware.TeclaPulsada(Sdl.SDLK_t);

				//Mover a las abejas donde está kibus
				foreach(Abeja abejini in abejinis)
				{
					abejini.Mover(kibus.X, kibus.Y);
				}
				
				DibujarTodo();
				Hardware.EscribirTexto("Buscando Casa", 641, 10); 
				Hardware.RefrescarPantalla();
				
				//Que cada una intente moverse 5 veces para encontrar lo mas calido
				for(int i = 0; i < RITUALES; i++)
				{
					foreach(Abeja abejini in abejinis)
					{
						direccion = (Direccion)random.Next(0, (int)Direccion.MISINGNO);
						
						while(!IntentarMover(abejini, direccion, false))
						{
							direccion = (Direccion)random.Next(0, (int)Direccion.MISINGNO);
						}
						
						//TODO: elegir la temperatura 
						abejini.Mover(direccion, temperatura[abejini.OnToyX][abejini.OnToyY]);
					}
					DibujarTodo();
					Hardware.RefrescarPantalla();
					Hardware.Pausar(1);
				}
				
				//Iterar entre todas las abejas y elegir la que alcanzó mayor temperatura.
				foreach(Abeja abejini in abejinis)
				{
					if(abejini.TemperaturaAlcanzada >= elegible.TemperaturaAlcanzada)
					{
						elegible = abejini;
					}
				}
				
				
				while(elegible.Propagacion.Count != 0)
				{
					kibus.Mover(elegible.Propagacion.Dequeue());

					DibujarTodo();
					Hardware.RefrescarPantalla();
					Hardware.Pausar(1);
					
					if(kibus.OnToyX == casa.OnToyX && kibus.OnToyY == casa.OnToyY)
					{
						casaEncontrada = true;
						break;
					}
				}
				
				temperatura[kibus.OnToyX][kibus.OnToyY] -= 30;
				temperaturas[kibus.OnToyY + kibus.OnToyX * 20].Calor -= 30; 
				temperaturas[kibus.OnToyY + kibus.OnToyX * 20].ActualizarTemperatura();
				//reiniciar recorridos
				foreach(Abeja abejini in abejinis)
				{
					abejini.reiniciarPropagacion();
				}
			}while(!casaEncontrada);
		}
		
		private void CalentarCeldas()
		{
			temperatura = new int[20][];
			
			for(int i = 0; i < 20; i++)
			{
				temperatura[i] = new int[20];
			}
			
			temperatura[this.casa.OnToyX][ this.casa.OnToyY] = 500;
			
			calentarEnOnda(this.casa.OnToyX, this.casa.OnToyY, temperatura[this.casa.OnToyX][ this.casa.OnToyY]);
			
			/*if(this.casa.OnToyY + 1 < 20)
				Amplitud(this.casa.OnToyX, this.casa.OnToyY + 1, temperatura[this.casa.OnToyX][this.casa.OnToyY]);
			if(this.casa.OnToyY - 1 >= 0)
				Amplitud(this.casa.OnToyX, this.casa.OnToyY - 1, temperatura[this.casa.OnToyX][this.casa.OnToyY]);
			if(this.casa.OnToyX + 1 < 20)
				Amplitud(this.casa.OnToyX + 1, this.casa.OnToyY, temperatura[this.casa.OnToyX][this.casa.OnToyY]);
			if(this.casa.OnToyX - 1 >= 0)
				Amplitud(this.casa.OnToyX - 1, this.casa.OnToyY, temperatura[this.casa.OnToyX][this.casa.OnToyY]);*/


			temperaturas = new List<Temperatura>();

			for(int i = 0; i < 20; i++)
			{
				for(int j = 0; j < 20; j++)
				{
					temperaturas.Add(new Temperatura(i, j, temperatura[i][j]));
				}
			}
		}
		
		private void Amplitud (int x, int y, int tempActual)
		{
			if(tempActual < 0) tempActual = 1;
			if (mapa [x][y] == -1) return;
			temperatura[x][y] = tempActual - 1;

			if (x > 0) 
			{
				if (temperatura[x - 1][y] == 0) Amplitud (x - 1, y, temperatura[x][y]);
			}
			
			if (x + 1 < 20) {
				if (temperatura[x + 1][y] == 0) Amplitud (x + 1, y, temperatura[x][y]);
			}
			
			if (y > 0) {
				if (temperatura[x][y - 1] == 0) Amplitud (x, y - 1, temperatura[x][y]);
			}
			
			if (y + 1 < 20) {
				if (temperatura[x][y + 1] == 0) Amplitud (x, y + 1, temperatura[x][y]);
			}
		}
		
		protected override void DibujarTodo()
		{
			Hardware.DibujarFondo();
			
			DibujarObstaculos();
			if(colorearTemperatura) Hardware.DibujarTemperaturas(temperaturas);
			kibus.Dibujar();
			DibujarAbejinis();
			casa.Dibujar();
			
			Hardware.RefrescarPantalla();
		}
		
		private void DibujarAbejinis()
		{
			foreach(Abeja abejini in abejinis)
			{
				abejini.Dibujar();
			}
		}
		
		private void calentarEnOnda(int x, int y, int temperatura)
		{
			int x1;
			int x2;
			int y1;
			int y2;
			
			x1 = x2 = x;
			y1 = y2 = y;
			
			do
			{
				x1 = (x1 > 0) ? x1 - 1 : 0 ; // Se reduce x si no sale de matriz
				y1 = (y1 > 0) ? y1 - 1 : 0 ; // Se reduce y si no sale de matriz
                x2 = (x2 < 20) ? x2 + 1 : 20 ; // Se aumenta x si no sale de matriz
                y2 = (y2 < 20) ? y2 + 1 : 20 ; // Se aumenta y si no sale de matriz
				
				if(x1 >= 0)
				{
                	rellenaX(temperatura, y1, y2, x1);
                }
				
                if(x2 != 20)
				{
                	rellenaX(temperatura, y1, y2, x2);
                }
				
                if(y1 >= 0)
				{
                	rellenaY(temperatura, x1, x2, y1);
                }
				
                if(y2 != 20)
				{
                	rellenaY(temperatura, x1, x2, y2);
                }
				
                temperatura -= 10;
				
			}while(x1 > 0 || x2 < 20 || y1 > 0 || y2 < 20);
			
		}
		
		private void rellenaX(int tempActual, int y1, int y2, int x)
		{
			while(y1 <= y2 && y2 != 20)
			{
            	if(mapa [x][y1] != -1 && temperatura[x][y1] < tempActual)
				{
                	temperatura[x][y1] = tempActual;
                }
                y1++;
            }
		}
		
		private void rellenaY(int tempActual, int x1, int x2, int y)
		{
			while(x1 < x2 )
			{
            	if(mapa [x1][y] != -1 && temperatura[x1][y] < tempActual)
				{
                	temperatura[x1][y] = tempActual;
                }
                x1++;
            }
		}
		
		/*public void fillExpand(int x, int y){
                System.out.println("Llenando en expansión");
                int x1, x2, y1, y2, calor = 100;
                x1 = x2 = x;
                y1 = y2 = y;
                //boolean fill = false;
                do{
                        x1 = (x1 > 0) ? x1 - 1: 0 ; // Se reduce x si no sale de matriz
                        x2 = (x2 < X-1) ? x2 + 1: X-1 ; // Se aumenta x si no sale de matriz
                        y1 = (y1 > 0) ? y1 - 1: 0 ; // Se reduce y si no sale de matriz
                        y2 = (y2 < Y-1) ? y2 + 1: Y-1 ; // Se aumenta y si no sale de matriz
                        System.out.println("X: "+x+" x1: "+x1+" x2: "+x2);
                        System.out.println("Y: "+y+" y1: "+y1+" y2: "+y2);
                        if(x1 >= 0){
                                System.out.println("Llenado 1");
                                fillX(calor, y1, y2, x1);
                        }
                        if(x2 != X){
                                System.out.println("Llenado 2");
                                fillX(calor, y1, y2, x2);
                        }
                        if(y1 >= 0){
                                System.out.println("Llenado 3");
                                fillY(calor, x1, x2, y1);
                        }
                        if(y2 != Y){
                                System.out.println("Llenado 4");
                                fillY(calor, x1, x2, y2);
                        }
                        calor-=1;
                        /*for(int i = 0; i < Y; i++){
                                for(int j = 0; j < X; j++){
                                        System.out.print(MAT[j][i]+" ");
                                }
                                System.out.println();
                        }*/
                /*}while(x1 > 0 || x2 < X-1 || y1 > 0 || y2 < Y-1);
        }

        public void fillX(int value, int y1, int y2, int x){
                while(y1 <= y2){
                        if(canMoveTo(x, y1) && MAT[x][y1] < value){
                                MAT[x][y1] = value;
                        }
                        y1++;
                }
        }
        
        public void fillY(int value, int x1, int x2, int y){
                while(x1 <= x2){
                        if(canMoveTo(x1, y) && MAT[x1][y] < value){
                                MAT[x1][y] = value;
                        }
                        x1++;
                }
        }*/
	}
}
