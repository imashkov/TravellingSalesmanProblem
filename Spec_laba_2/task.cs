using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Spec_laba_2
{
    public class TransportTask
    {
        public int N;
        public List<int> directive_time { get; }
        public List<List<int>> time { get; }

        public TransportTask(string path)
        {
            List<int> l = new List<int>() { };
            this.directive_time = l;
            this.time = new List<List<int>> { };
            using (StreamReader sr = new StreamReader(path))
            {
                int t = 0;
                string[] tmp = null;
                this.N = Convert.ToInt32(sr.ReadLine());
                string line = sr.ReadLine();
                tmp = line.Split(' ');
                for (int i = 0; i < N; i++)
                    directive_time.Add(Convert.ToInt32(tmp[i]));
                line = sr.ReadToEnd();
                tmp = line.Split('\t');
                for (int i = 0; i < N + 1; i++)
                {
                    time.Add(new List<int> { });
                    for (int j = 0; j < N + 1; j++)
                    {
                        time[i].Add(Convert.ToInt32(tmp[t]));
                        t++;
                    }
                }
            }
        }

        public int BaseCountB(List<int> v)
        {
            List<int> free_leaves = new List<int> { };
            List<int> V = v.GetRange(0, v.Count);
            int min = Int32.MaxValue, curr_min_peak = 0, B = 0;
            int current_time = time[0][V[0]];
            if (current_time > directive_time[V[0] - 1])
                B++;
            for (int i = 0; i < V.Count - 1; i++)
            {
                current_time += time[V[i]][V[i + 1]];
                if (current_time > directive_time[V[i + 1] - 1])
                    B++;
            }
            for (int i = 1; i < N + 1; i++)
            {
                if (!V.Contains(i))
                {
                    free_leaves.Add(i);
                }
            }
            while (free_leaves.Count != 0)
            {
                for (int j = 0; j < free_leaves.Count; j++)
                {
                    if (time[V.Last()][free_leaves[j]] < min)
                    {
                        min = time[V.Last()][free_leaves[j]];
                        curr_min_peak = free_leaves[j];
                    }
                }
                V.Add(curr_min_peak);
                free_leaves.Remove(curr_min_peak);
                current_time += min;
                if (current_time > directive_time[curr_min_peak - 1])
                    B++;
                min = Int32.MaxValue;
            }
            return B;
        }

        public int BaseCountH(List<int> v)
        {
            List<int> free_leaves = new List<int> { };
            List<int> V = v.GetRange(0, v.Count);
            int H = 0;
            int current_time = time[0][V[0]];
            if (current_time > directive_time[V[0] - 1])
                H++;
            for (int i = 0; i < V.Count - 1; i++)
            {
                current_time += time[V[i]][V[i + 1]];
                if (current_time > directive_time[V[i + 1] - 1])
                    H++;
            }
            for (int i = 1; i < N + 1; i++)
            {
                if (!V.Contains(i))
                    free_leaves.Add(i);
            }
            for (int i = 0; i < free_leaves.Count; i++)
            {
                if (current_time + time[V.Last()][free_leaves[i]] > directive_time[free_leaves[i] - 1])
                    H++;
            }
            return H;
        }
        public int ResearchCountH(List<int> v)
        {
            List<int> free_leaves = new List<int> { };
            List<int> V = v.GetRange(0, v.Count);
            int Hmin = Int32.MaxValue;
            int currentH = 0;
            int H = 0;
            int current_time = time[0][V[0]];
            if (current_time > directive_time[V[0] - 1])
                H++;
            for (int i = 0; i < V.Count - 1; i++)
            {
                current_time += time[V[i]][V[i + 1]];
                if (current_time > directive_time[V[i + 1] - 1])
                    H++;
            }
            for (int i = 1; i < N + 1; i++)
            {
                if (!V.Contains(i))
                    free_leaves.Add(i);
            }
            if (free_leaves.Count == 0)
                Hmin = H;
            for (int i = 0; i < free_leaves.Count; i++)
            {
                current_time += time[V.Last()][free_leaves[i]];
                currentH = H;
                if (current_time > directive_time[free_leaves[i] - 1])
                    currentH++;
                for (int j = 0; j < free_leaves.Count; j++)
                {
                    if ((i != j) && (current_time + time[free_leaves[i]][free_leaves[j]] > directive_time[free_leaves[j] - 1]))
                        currentH++;
                }
                if (currentH < Hmin)
                    Hmin = currentH;
                current_time -= time[V.Last()][free_leaves[i]];
            }
            return Hmin;
        }
    }
}