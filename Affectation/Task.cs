using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affectation
{
    class Task
    {
        protected List<List<int>> tache { get; set; } = new List<List<int>>()
        {
            new List<int>() {10,5,9,18,11},
            new List<int>() {13,19,6,12,14},
            new List<int>() {3,2,4,4,5},
            new List<int>() {18,9,12,17,15},
            new List<int>() {11,6,14,19,10}
        };
        protected int mr;
        protected List<int> indexLastC = new List<int>(){0,0};
        protected List<int> baseMinIndex = new List<int>();

        protected List<int> A;
        protected List<int> A_;

        protected List<int> indexBase;

        public Task()
        {
            this.A = new List<int>();
            this.A_ = new List<int>();
        }

        public void init()
        {
            this.printTache();
            this.checkMin();
            this.printMinIndex();
            this.insertBaseToA();
            this.getMinMr();
            this.incBaseForMr();
            this.printTache();
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
            Console.WriteLine("Base initiale :" + this.getBaseInitial());
            this.A.Add(this.getBaseInitial());
        }

        public void getMinMr()
        {
            int min = 99999;
            for(int i = 0; i < this.tache.Count;i++)
            {
                for(int j = 0;j<this.A.Count;j++)
                {

                    var a = this.getMinRowIndexWithException(this.tache[i], this.A);
                    // Console.WriteLine("Base :" + this.A[i]);
                    if (min > this.tache[i][a] - this.tache[i][this.baseMinIndex[i]])
                    {
                        this.mr = this.tache[i][a] - this.tache[i][this.baseMinIndex[i]];
                        this.indexLastC[0] = i;
                        this.indexLastC[1] = this.baseMinIndex[i];

                        Console.WriteLine("Last CR :" + this.baseMinIndex[4]);
                        min = this.mr;
                    }
                }
            }

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

        public void checkMin()
        {
            for(int i = 0;i<this.tache.Count;i++)
            {
                int min = 0;
                this.baseMinIndex.Add(this.getMinRowIndex(this.tache[i]));
                Console.WriteLine(this.baseMinIndex[i]);
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

        public int getMinRowIndexWithException(List<int> list,List<int> indexIgnore)
        {
            var indexMin = 0;
            for (int i = 1; i < list.Count && !indexIgnore.Contains(i); i++)
            {
                if (compare(list[indexMin], list[i]))
                {
                    indexMin = i;
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

            Console.WriteLine("  T1 T2 T3 T4 T5");
            for (int i = 0; i<this.tache.Count;i++)
            {
                Console.WriteLine("M" + i + 1+" ");
                for(int j = 0;j<this.tache[i].Count;j++)
                {
                    Console.Write("  " + this.tache[i][j]);
                }
                Console.WriteLine();
            }
        }

    }
}
