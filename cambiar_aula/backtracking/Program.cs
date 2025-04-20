using System;


public class Program
{
    public static void Main()
    {
        //Primera Parte: Obtener los datos
        //un numero inicial que indica cuantos grillas hay
        int cantidadGrillas = Convert.ToInt32(Console.ReadLine());
        //Armar una cola de grillas
        Queue<int[,]> grillas = new Queue<int[,]>();

        while (cantidadGrillas > 0) //O(cG * n * m)
        {
            //inserto dimension
            int[] dimensionGrilla = new int[2];
            dimensionGrilla = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);


            //las creo
            int[,] grilla = crearGrilla(dimensionGrilla[0], dimensionGrilla[1]); //O(n*m)
            grillas.Enqueue(grilla);

            cantidadGrillas--;
        }

        //ejecuto backtracking
        while (grillas.Count > 0){ //O(cG * n * m)
            var grilla = grillas.Dequeue();
            Console.WriteLine(backtracking(grilla));
        }


        //crea la grilla
        int[,] crearGrilla(int n, int m) //O(n*m)
        {
            int[,] res = new int[n, m]; //grilla de 2 dimensiones
            //leer cada fila
            for (int i = 0; i < n; i++) //O(n)
            {
                //asumo que linea tendra justo m elementos
                string[] linea = Console.ReadLine().Split(' ');
                //añado cada elemento a la linea
                for (int j = 0; j < m; j++) //O(m)
                {
                    res[i, j] = int.Parse(linea[j]);
                }
            }
            return res;
        }

        //procesa la grilla 
        string backtracking(int[,] grilla) //O(n * m) ya que buscarCamino puede tardar como max. recorrer toda la tabla.
        {
            int valorCamino = grilla[0, 0];
            (int, int) posicion = (0, 0); //posicion actual
            //array que contenga las posiciones de grilla que visitaste
            //diccionario para memorizacion (una misma posicion puede tener multiples estados segun valorCamino en el momento)
            HashSet<(int, int, int)> memorizacion = new HashSet<(int, int, int)>();



            bool hayCamino = buscarCamino(grilla, posicion, valorCamino, memorizacion);
            if (hayCamino) { return "YES"; } else { return "NO"; }

            
        }

        //recursion
        bool buscarCamino(int[,] grilla, (int, int) posicion, int valorCamino, HashSet<(int, int, int)> memo)
        {
            int filas = grilla.GetLength(0);
            int columnas = grilla.GetLength(1);
            int x = posicion.Item1;
            int y = posicion.Item2;
            List<(int, int)> direcciones = new List<(int, int)>
            {
                    (1,0), //abajo
                    (0,1) //derecha
            };
            int distanciaRestante = Math.Abs((filas - 1) - x) + Math.Abs((columnas - 1) - y);


            //si la posicion da falso en nuestro valorCamino actual, saltearla
            if (memo.Contains((x,y, valorCamino))) {
                return false;
            }

            //si ya habia valores antes, este lo suma a la lista
            memo.Add((x,y, valorCamino));

            //si los pasos que hay que dar desde (0,0) excluyendolo son impares, no llega a 0
            if ((filas + (columnas - 1)) % 2 != 0)
            {
                return false;
            }

            //caso base (la posicion es igual al destino final)
            if (posicion == (filas - 1, columnas - 1))
            {
                if (valorCamino == 0) {
                    return true;
                }
            }


            //poda 1: si el absoluto del valorCamino > a la cantidad de casillas que hay de distancia hasta el destino.
            if (Math.Abs(valorCamino) > distanciaRestante)
            {
                return false;
            }
            //optimizar direccion mas corta
            if ((filas - 1) - x > (columnas - 1) - y) {
                direcciones.Reverse();
            }
            //recursion (viaja direcciones)
            foreach (var dir in direcciones)
            {
                //actualizar posicion
                int nuevoX = x + dir.Item1;
                int nuevoY = y + dir.Item2;
                //que este dentro del mapa, hace Depth-First Search ya que no cambia la direccion hasta que no este en el mapa
                if (nuevoX < filas && nuevoY < columnas)
                {
                    int nuevoValorCamino = valorCamino + grilla[nuevoX, nuevoY];
                    if (buscarCamino(grilla, (nuevoX, nuevoY), nuevoValorCamino, memo))
                    {
                        return true;
                    }
                }
            }

            //cuando llega a un final y valorCamino != 0

            return false;
        }
    }
}