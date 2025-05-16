using System;

public class SumaDinamica
{
    static void Main()
    {
        //bottomUp
        /*
         * 1) subset_sum(C, k): // computa M [i, j] para todo 0 ≤ i ≤ n y 0 ≤ j ≤ k.
            2) Inicializar M [0, j] := (j = 0) para todo 0 ≤ j ≤ k.
            3) Para i = 1, . . . , n y para j = 0, . . . , k:
            4) Poner M [i, j] := M [i − 1, j] ∨ (j − C[i] ≥ 0 ∧ M [i − 1, j − C[i]])
        */
        int[] C = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        int k = Convert.ToInt32(Console.ReadLine());
        int n = C.Length;

        //llamada a la funcion
        Console.WriteLine(subset_sum(C, k));
        
        //Optimizar:
        /* Este codigo cuenta 52 veces, pero en M estoy generando casos donde se que no pasaremos por ahi 
         * si armo el arbol del algoritmo, por ejemplo nosotros solo recorremos los i = 0 que sean (0,12), (0,6), (0,0), pero generamos todos los demas 
           innecesariamente
         */ 

        //basicamente, estoy construyendo todas las filas del DP (desde abajo para arriba), pero solo algunas combinaciones

        bool subset_sum(int[] C, int k) //O(nk)
        {
            bool[] previo = new bool[k + 1]; //O(k)
            bool[] actual = new bool[k + 1]; //O(k)

            previo[0] = true;

            /*no optimizado
            //los casos donde ya se vieron todos los elementos
            for (int j = 0; j <= k; j++) //O(k)
            {
                M[(0, j)] = (j == 0);
            }
            */
            //los casos donde aun se puede restar
            for (int i = 1; i <= n; i++)// O(n) * O(k)
            {
                //todos los elementos de la lista
                for (int j = 0; j <= k; j++) //O(k)
                {
                    actual[j] = previo[j]; //si no va a restar, se queda con el anterior
                    //si el resto no es negativo, se puede seguir restando, actua de poda
                    if (j - C[i - 1] >= 0)
                    {
                        if (actual[j] == false)
                        {
                            //si el anterior es verdadero, se puede restar
                            actual[j] = previo[j - C[i - 1]];
                        }
                    }
                }
                //para poder retornar el resultado, guardamos el anterior
                var temp = previo;
                previo = actual;
                actual = temp;

            }

            return previo[k];
        }
    }
}