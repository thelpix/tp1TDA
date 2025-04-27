using System;


public class SumaDinamica
{
    public static void Main()
    {
        /*
         * 
         * 1) Inicializar M [i, j] = ⊥ para todo 0 ≤ i ≤ n y 0 ≤ j ≤ k.
            2) subset_sum(C, i, j): // implementa ss({c1, . . . , ci}, j) = ss’C (i, j) usando memoización
            3) Si j < 0, retornar falso
            4) Si i = 0, retornar (j = 0)
            5) Si M [i, j] = ⊥:
            6) Poner M [i, j] = suset_sum(C, i − 1, j) ∨ subset_sum(C, i − 1, j − C[i])
            7) Retornar M [i, j]
        */
        int[] C = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        int j = Convert.ToInt32(Console.ReadLine());
        int i = C.Length;
        int[] p = new int[i]; //solucion parcial
        Dictionary<(int, int), bool> M = new Dictionary<(int, int), bool>();

        //llamada a la funcion
        if (subset_sum(C, i, j, M, p))
        {
            //imprimir elemento por elemento
            Console.WriteLine("Los numeros que dan " + j + "son: ");
            for (int n = 0; n < p.Length; n++)
            {
                Console.Write(p[n] + ", ");
            }
            Console.Write(p[p.Length - 1]);
        }
        else
        {
            Console.WriteLine("No hay :(");
        }

        bool subset_sum(int[] C, int i, int j, Dictionary<(int, int), bool> M, int[] p)
        {
            if (j < 0) { return false; }
            if (i == 0) { return (j == 0); }

            if (!M.ContainsKey((i, j))) {
                //LO PARTICIONAMOS EN DOS CASOS, IZQUIERDA DEL ARBOL Y DERECHA
                //si no se cumple ninguna regla, se hace la llamada a la funcion
                //se guarda el elemento en el vector de solucion parcial

                //si no tomamos j - C[i-1], entonces p[i-1] = 0
                if (subset_sum(C, i - 1, j, M, p))
                {
                    //si se cumple la condicion, entonces se guarda el elemento en el vector de solucion parcial
                    p[i - 1] = 0;
                    M[(i, j)] = true;
                }

                //si tomamos j - C[i-1], entonces p[i-1] = C[i-1]
                else if (subset_sum(C, i - 1, j - C[i - 1], M, p))
                {
                    p[i - 1] = C[i - 1];
                    M[(i, j)] = true;
                }
                else
                {
                    M[(i, j)] = false;
                }
            }

            M.TryGetValue((i, j), out bool valor);
            return valor;
        }
    }
}