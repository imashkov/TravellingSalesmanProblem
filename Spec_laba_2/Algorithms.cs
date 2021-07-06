using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Spec_laba_2
{
    public class Path
    {
        private TransportTask task;
        public List<int> V { get; }
        public int B;
        public int H;

        public Path(TransportTask task, int type, List<int> v)
        {
            this.task = task;
            this.V = v;
            this.B = this.task.BaseCountB(v);
            if (type == 0)
                this.H = this.task.BaseCountH(v);
            if (type == 1)
                this.H = this.task.ResearchCountH(v);
        }
    }
    public class Method
    {
        private TransportTask task;
        private List<Path> tree { get; set; }
        public int count_of_peaks = 0;
        private int type;  // 0 - base, 1 - research
        public string elapsed_time;
        public long elapsed_ticks;

        public Method(TransportTask task, int type)
        {
            this.tree = new List<Path>() { };
            this.task = task;
            this.type = type;
            Stopwatch stop_watch = new Stopwatch();
            stop_watch.Start();
            InitialBranching();
            Check();
            while (IsTreeVaried())
            {

                Branching();
                Check();
            }
            stop_watch.Stop();
            this.elapsed_ticks = stop_watch.ElapsedTicks;
            this.elapsed_time = String.Format("{0:mm\\:ss\\.fffffff}", stop_watch.Elapsed);
        }
        private bool IsTreeVaried()
        {
            for (int i = 0; i < tree.Count; i++)
            {
                if (tree[i].B != tree[i].H)
                    return true;
                if (tree[0].B != tree[i].B)
                    return true;
            }
            return false;
        }

        public void PrintTree()
        {
            Console.WriteLine();
            if (type == 1) 
            Console.WriteLine("Moe DREVO:");
            if (type == 0)
                Console.WriteLine("Bazovoe DREVO:");
            for (int i = 0; i < tree.Count; i++)
            {
                for (int j = 0; j < tree[i].V.Count; j++)
                    Console.Write(tree[i].V[j] + " ");
                Console.WriteLine();
                Console.Write("  " + tree[i].B + "  " + tree[i].H);
                Console.WriteLine();
            }
        }

        private void InitialBranching()
        {
            for (int i = 1; i < task.N + 1; i++)
                    tree.Add(new Path(task, type, new List<int> { i }));
            count_of_peaks += task.N;
        }

        private void Branching()
        {
            List<int> free_leaves = new List<int> { };
            int j = 0;
            if (type == 0)
                while (tree[j].V.Count() == task.N)
                {
                    j++;
                }
            if (type == 1)
            {
                while (tree[j].B == tree[j].H)
                {
                    j++;
                }
            }
            List<int> ParentV = tree[j].V.GetRange(0, tree[j].V.Count);
            tree.RemoveAt(j);
            for (int i = task.N; i > 0; i--)
            {
                if (!ParentV.Contains(i))
                {
                    free_leaves.Add(i);
                }
            }
            count_of_peaks += free_leaves.Count;
            for (int i = 0; i < free_leaves.Count; i++)
            {
                ParentV.Add(free_leaves[i]);
                if (type == 0)
                    tree.Insert(j, new Path(task, 0, ParentV.GetRange(0, ParentV.Count)));
                else tree.Add(new Path(task, 1, ParentV.GetRange(0, ParentV.Count)));
                ParentV.Remove(ParentV.Last());
            }
        }

        private void Check()
        {
            int Bmin = Int32.MaxValue, peak_with_Bmin = 0;
            List<int> Deleting = new List<int> { };
            for (int i = 0; i < tree.Count; i++)
            {
                if (tree[i].B < Bmin)
                {
                    Bmin = tree[i].B;
                    peak_with_Bmin = i;
                }
            }
            for (int i = 0; i < tree.Count; i++)
            {
                if ((tree[i].H >= Bmin) && (i != peak_with_Bmin))
                    Deleting.Add(i);
            }
            for (int i = Deleting.Count - 1; i > -1; i--)
                tree.RemoveAt(Deleting[i]);
        }
    }
}
