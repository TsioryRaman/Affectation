using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affectation
{
    class Task
    {
        protected List<List<int>> tacheOriginal { get; set; }
        protected List<List<int>> tache { get; set; } = new List<List<int>>()
        {
            new List<int>() {10,5,9,18,11},
            new List<int>() {13,19,6,12,14},
            new List<int>() {3,2,4,4,5},
            new List<int>() {0,9,12,17,15},
            new List<int>() {11,6,14,19,10}
        };
        protected int mr;
        protected List<int> D = new List<int>();
        protected List<int> indexLastC = new List<int>(){0,0};
        protected List<List<int>> listIndexC = new List<List<int>>();
        protected List<int> baseMinIndex = new List<int>();

        protected List<int> A;
        protected List<int> A_;

        protected List<int> indexBase;

        public Task()
        {
            this.A = new List<int>();
            this.A_ = new List<int>();
            this.tacheOriginal = this.tache;
        }

        public void init()
        {
            var i = 0;
            while(this.checkIfFinish() || i==0)
            {
                Console.WriteLine("INDEX : " + i);
                this.printTache();
                if(i < 1)
                {
                    this.checkMin();
                }
                this.printMinIndex();
                this.insertBaseToA();
                this.printBase();
                this.initHorsBase();
                this.getMinMr();
                this.incBaseForMr();
                Console.WriteLine("Incrementer chaque base de mr: " +this.mr);
                this.printTache();
                // Si la colonne de C contient d'autre base
                while (this.checkIfCContainOtherBase())
                {

                    this.A.Add(this.indexLastC[1]);
                    this.A_.Remove(this.indexLastC[1]);
                    Console.WriteLine("===============================================================================");
                    Console.WriteLine("Contient d'autre base");
                    this.getMinMr();
                    this.printMinIndex();
                    this.incBaseForMr();
                    Console.WriteLine("Incrementer chaque base de mr: "+ this.mr);
                    this.printTache();
                    
                }
                Console.WriteLine("+++++++++++++++++++++++++++++++FIND AND DELETE BASE+++++++++++++++++++++++++++++++");
                this.findLastBaseAndDelete();
                Console.WriteLine("Last C index :" + this.indexLastC[0] + " colonne de C :" + this.indexLastC[1]);
                this.checkIfReRunAlgorithm();
                this.printTache();
                this.printMinIndex();
                i++;
            }
            this.printResult();
        }

        public void printBase()
        {
            Console.Write("LES BASES : ");
            for(int i = 0;i<this.A.Count;i++)
            {
                Console.Write(this.A[i] + " -");
            }
        }
        public int getBaseInitial()
        {
            var _tmp = this.baseMinIndex.GroupBy(i => i);
            int _t = 0;
            int key = 0;
            foreach (var t in _tmp)
            {
                if (_t < t.Count())
                {
                    _t = t.Count();
                    key = t.Key;
                }
            }
            return key;
        }
        public void insertBaseToA()
        {
            this.A.Add(this.getBaseInitial());
        }

        public void getMinMr()
        {
            Console.WriteLine("Avoir le MR minimum");
            // Max d'entier
            int min = int.MaxValue;
            int result = 0;
            for(int i = 0; i < this.tache.Count;i++)
            {

                for(int j = 0;j<A.Count;j++)
                {
                    if(this.baseMinIndex[i] == this.A[j])
                    {
                        var a = this.getMinRowIndexWithException(this.tache[i]);
                        result = this.tache[i][a] - this.tache[i][this.baseMinIndex[i]];
                        if (min > result)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Min ligne :" + i + " a la colonne " + a);

                            Console.ResetColor();

                            this.indexLastC[0] = i;
                            this.indexLastC[1] = a;


                            min = result;
                        }
                    }
                }

            }

            this.mr = min;

            Console.WriteLine("Mr a la ligne " + this.indexLastC[0] + "a la colonne : " + this.indexLastC[1]);
            this.listIndexC.Add(this.indexLastC);

        }

        // Initialisation de A'.
        public void initHorsBase()
        {
            Console.WriteLine("\nInsertion des hors-base :");
            this.A_.Clear();
            for (int i = 0;i<this.tache[0].Count && !this.A.Contains(i);i++)
            {
                this.A_.Add(i);
            }
        }
        
        // Cherche d'autre base.
        public bool checkIfCContainOtherBase()
        {
            for(int i = 0;i <this.baseMinIndex.Count;i++)
            {
                    if (this.baseMinIndex[i] == this.indexLastC[1])
                    {
                        return true;
                    }
            }

            return false;
        }
        // Etape 6/7
        public void findLastBaseAndDelete()
        {
            // Mettre la base en tant que D
            this.D.Add(this.baseMinIndex[this.indexLastC[0]]);
            this.D.Add(this.baseMinIndex[this.indexLastC[1]]);

            // Supprimer C de la liste des elements pointilles
            this.listIndexC.Remove(this.indexLastC);

            // Mettre C en tant que base
            this.baseMinIndex[this.indexLastC[0]] = this.indexLastC[1];
        }

        // Check si chaque ligne a respectivement une seule colonne
        public bool checkIfFinish()
        {
            var finals = this.baseMinIndex.GroupBy(i => i);
            foreach (var f in finals)
            {
                if(f.Count() > 1)
                {
                    // this.baseMinIndex.Clear();
                    return true;
                }
            }
            return false;
        }

        public bool checkIfCInDColumn()
        {
            for(int i = 0;i<this.listIndexC.Count;i++)
            {
                if (this.listIndexC[i][1] == this.D[1])
                {
                    this.indexLastC = this.listIndexC[i];
                    return true;
                }
            }
            this.indexLastC = null;
            return false;
        }

        public void checkIfReRunAlgorithm()
        {

            // Si ne contient pas d'autre base et checker si la colonne D a encore des elements pointilles
            if(!this.baseMinIndex.Contains(this.D[1]) && this.checkIfCInDColumn())
            {

                this.D.Clear();
                this.findLastBaseAndDelete();
            }
            this.A.Clear();
            this.D.Clear();
            
        }

        public void incBaseForMr()
        {
            for(int i = 0; i<this.tache.Count;i++)
            {
                for(int j = 0;j < this.A.Count;j++)
                {
                    this.tache[i][this.A[j]] = this.tache[i][this.A[j]] + this.mr;
                }
            }
        }

        // Trouver la base
        public void checkMin()
        {
            Console.WriteLine("Index de chaque minimum sur chaque ligne");
            this.baseMinIndex.Clear();
            for(int i = 0;i<this.tache.Count;i++)
            {
                this.baseMinIndex.Add(this.getMinRowIndex(this.tache[i]));
            }
        }

        

        public void printMinIndex()
        {
            for (int i = 0; i < tache.Count; i++)
            {
                Console.WriteLine(i + " : " + this.baseMinIndex[i]);
            }
        }

        public int getMinRowIndex(List<int> list)
        {
            var indexMin = 0; 
            for(int i = 1;i<list.Count;i++)
            {
                if(compare(list[indexMin],list[i]))
                {
                    indexMin = i;
                }
            }

            return indexMin;
        }

        public int getMinRowIndexWithException(List<int> list)
        {
            var indexMin = 0;
            for (int i = 0;i<list.Count; i++)
            {

                if (!this.A.Contains(i))
                {
                    indexMin = i;
                    break;
                }
            };
            for (int i = indexMin + 1; (i < (list.Count - 1)); i++)
            {
                if(!this.A.Contains(i))
                {
                    if (compare(list[indexMin], list[i]))
                    {
                        indexMin = i;
                    }
                }
            }

            return indexMin;
        }

        public bool compare(int a,int b)
        {
            if(b < a)
            {

                return true;
            }

            return false;

        }

        public void printTache()
        {

            Console.WriteLine("   T1 T2 T3 T4 T5");
            for (int i = 0; i<this.tache.Count;i++)
            {
                Console.Write("M" + (i + 1));
                for(int j = 0;j<this.tache[i].Count;j++)
                {
                    Console.Write(" " + this.tache[i][j]);
                }
                Console.WriteLine();
            }
        }

        public void printResult()
        {
            for (int i = 0; i < this.tacheOriginal.Count;i++)
            {
                Console.WriteLine("M" + i + " ==> T" + this.baseMinIndex[i]);
            }
            int s = 0;
            for (int i = 0; i < this.tache.Count; i++)
            {
                s = s + this.tacheOriginal[i][this.baseMinIndex[i]];
            }

            Console.WriteLine("L'heure total est de : " + s + " heures");
        }
    }

}
