using System;

public class MagiCuadrados
{
    static void Main()
    {
        /*
         * Un cuadrado mágico de orden n, es un cuadrado con los números {1, . . . , n^2}, tal que todas sus
            filas, columnas y las dos diagonales suman lo mismo (ver figura). El número que suma cada fila
            es llamado número mágico.
            2 7 6
            9 5 1
            4 3 8
            Existen muchos métodos para generar cuadrados mágicos. El objetivo de este ejercicio es contar
            cuántos cuadrados mágicos de orden n existen.
         */
        Console.WriteLine("Ingrese el orden del cuadrado mágico:");
        int orden = int.Parse(Console.ReadLine());

        //creo un array de n^2, que es el cuadrado en sí.
        int[,] cuadrado = new int[orden, orden];

        //rellenar el cuadrado
        for (int i = 0; i < cuadrado.GetLength(0); i++) //O(n^2)
        {
            for (int j = 0; j < cuadrado.GetLength(1); j++) 
            {
                cuadrado[i, j] = 0;
            }
        }

        HashSet<int> num = new HashSet<int>();

        //numero magico por fila, numero de guass:
        int numMagico = (int) ((orden * (orden * orden + 1)) / 2); //O(1)

        Console.WriteLine("Hay " + cuadradosMagicos(cuadrado, 0, 0, num) + " cuadrados magicos");

        
        int cuadradosMagicos(int[,] cuadrado, int i, int j, HashSet<int> numerosUsados) //O(n^2) * O(n) = O((n^2)!)
        {

            //caso base
            //llega al final, validar el cuadrado (que las filas, columnas y diagonales sean igual a numMagico
            if (i == orden) //O(n^2)
            {
                // validar el cuadrado completo
                if (ValidarCuadrado(cuadrado)) 
                {
                    return 1;
                }

                return 0;
            }

            //recursion
            int cantCuadradosMagicos = 0;

            //repetir n^2 - numerosUsados veces
            for (int k = 1; k <= (orden * orden); k++) //O(n^2) * O(n)
            {
                //para no repetir numeros
                if (!numerosUsados.Contains(k)) //O(1) 
                {
                    cuadrado[i, j] = k;
                    numerosUsados.Add(k); //O(1)

                    //podar dentro de la recursion (no esta completo)
                    if(cuadradoValido(cuadrado, i, j)) //O(n)
                    {
                        if (j == orden - 1)
                        {
                            //si ya termino la fila
                            cantCuadradosMagicos += cuadradosMagicos(cuadrado, i + 1, 0, numerosUsados);

                        }
                        else
                        {
                            //sigue la fila
                            cantCuadradosMagicos += cuadradosMagicos(cuadrado, i, j + 1, numerosUsados);
                        }
                    }

                    //prepara el cuadrado para la siguiente combinacion
                    numerosUsados.Remove(k);
                    cuadrado[i, j] = 0;
                }
            }

            return cantCuadradosMagicos;
        }

        //usado en la recursion de cuadrados parciales
        bool cuadradoValido(int[,] cuadrado, int i, int j) //O(n)
        {
            //PODAS
            int filaParcial = 0;
            int columnaParcial = 0;

            //si la suma parcial de la fila no exceda el numMagico
            for (int k = 0; k <= j; k++) //O(n)
            {
                filaParcial += cuadrado[i, k];
            }

            //si la columna parcial de la columna no exceda el numMagico
            for (int k = 0; k <= i; k++) //O(n)
            {
                columnaParcial += cuadrado[k, j];
            }

            //que cuando la fila este completa este completa, que la fila sea numMagico
            if (j == (orden - 1) && (filaParcial != numMagico)) 
            {
                return false;
            }

            //que cuando la columna este completa este completa, que la columna sea numMagico
            if (i == (orden - 1) && (columnaParcial != numMagico)) 
            { 
                return false; 
            }

            //que las sumas parciales no superen el numMagico
            if (filaParcial > numMagico || columnaParcial > numMagico)
            {
                return false;
            }

            return true;
        }

        bool ValidarCuadrado(int[,] cuadrado) //O(n^2)
        {
            int sumaDiagonal1 = 0;
            int sumaDiagonal2 = 0;
            // Chequea todas las filas y columnas
            for (int i = 0; i < orden; i++) //O(n) * O(n) = O(n^2)
            {
                int sumaFila = 0, sumaColumna = 0;
                for (int j = 0; j < orden; j++) //O(n)
                {
                    sumaFila += cuadrado[i, j];
                    sumaColumna += cuadrado[j, i];
                }
                if (sumaFila != numMagico || sumaColumna != numMagico)
                {
                    return false;
                }
                sumaDiagonal1 += cuadrado[i, i]; // '\'
                sumaDiagonal2 += cuadrado[i, orden - 1 - i]; // '/'
            }
            // chequea la diagonal completa
            if (sumaDiagonal1 != numMagico || sumaDiagonal2 != numMagico) //O(1)
            {
                return false;
            }
            return true;
        }
    }
}
