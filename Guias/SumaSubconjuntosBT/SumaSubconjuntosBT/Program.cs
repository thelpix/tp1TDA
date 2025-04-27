using System;
using System.Numerics;

public class SumaSubconjuntosBT
{
    public static void Main()
    {
        /*
        * subset_sum(C, i, j): // implementa ss({c1, . . . , ci}, j)
            2) Si j < 0, retornar falso // regla de factibilidad
            3) Si i = 0, retornar (j = 0)
            4) Si no, retornar subset_sum(C, i − 1, j) ∨ subset_sum(C, i − 1, j − C[i])
         * Modificar la implementación para imprimir el subconjunto de C que suma k, si existe.
            Ayuda: mantenga un vector con la solución parcial p al que se le agregan y sacan los
            elementos en cada llamada recursiva; tenga en cuenta de no suponer que este vector se
            copia en cada llamada recursiva, porque cambia la complejidad.
        */
        //Escribir el conjunto de soluciones candidatas para C = {6, 12, 6} y k = 12
        Console.WriteLine("empieza");
        int[] C = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        int k = Convert.ToInt32(Console.ReadLine());
        int i = C.Length;
        int[] p = new int[i]; //solucion parcial
        int[] suma = sumaConjunto();

        int[] sumaConjunto()
        {
            int[] suma = new int[i];
            suma[0] = C[0];
            for (int j = 1; j < i; j++)
            {
                suma[j] = C[j] + C[j - 1];
            }
            return suma;
        }
        //llamada a la funcion
        if (subset_sum(C, i, k, p))
        {
            //imprimir elemento por elemento
            Console.WriteLine("Los numeros que dan " + k + "son: ");
            for (int j = 0; j < p.Length; j++)
            {
                Console.Write(p[j] + ", ");
            }
            Console.Write(p[p.Length - 1]);
        }
        else {
            Console.WriteLine("No hay :(");
        }

        bool subset_sum(int[] C, int i, int j, int[] p)
        {

            //regla de optimalidad
            if (j == 0)
            {
                return true;
            }

            //caso base
            if (i == 0)
            {
                return (j == 0);
            }

            //reglas de factibilidad
            if (j < 0)
            {
                return false;
            }
            //poda de imposibilidad, no llegara en el caso extremo a satisfacer j
            if (suma[i-1] < j)
            {
                return false;
            }


            //LO PARTICIONAMOS EN DOS CASOS, IZQUIERDA DEL ARBOL Y DERECHA
            //si no se cumple ninguna regla, se hace la llamada a la funcion
            //se guarda el elemento en el vector de solucion parcial

            //si no tomamos j - C[i-1], entonces p[i-1] = 0
            if (subset_sum(C, i - 1, j, p))
            {
                //si se cumple la condicion, entonces se guarda el elemento en el vector de solucion parcial
                p[i - 1] = 0;
                return true;
            }

            //si tomamos j - C[i-1], entonces p[i-1] = C[i-1]
            p[i-1] = C[i - 1];
            if(subset_sum(C, i - 1, j - C[i-1], p))
            {
                return true;
            }

            //backtracking por si da falso el camino derecho
            p[i - 1] = 0;
            return false;
        }
    }
}