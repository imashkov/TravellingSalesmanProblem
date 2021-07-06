using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spec_laba_2
{
    class Program
    {
        static void Main(string[] args)
        {
            float[] time_relation = new float[10];
            float[] peaks_relation = new float[10];
            string[] paths = { 
                @"task_2_01_n3.txt",
                @"task_2_02_n3.txt",
                @"task_2_03_n10.txt", 
                @"task_2_04_n10.txt", 
                @"task_2_05_n10.txt", 
                @"task_2_06_n15.txt",
                @"task_2_07_n15.txt",
                @"task_2_08_n50.txt", 
                @"task_2_09_n50.txt", 
                @"task_2_10_n50.txt" };

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("|{0, 14}{0, 14}{0, 14}{0, 14}{0, 14}{0, 14}{0, 14}{0, 14}", "|".PadLeft(14, '-'));
            Console.WriteLine("|{0, 13}|{1, 13}|{2, 13}|{3, 13}|{4, 13}|{5, 13}|{6, 13}|{7, 13}|",
                "Test", "Range", "Base time", "My time", "Relation", "Base V Count", "My V Count", "Relation");
            Console.WriteLine("|{0, 14}{0, 14}{0, 14}{0, 14}{0, 14}{0, 14}{0, 14}{0, 14}", "|".PadLeft(14, '-'));

            for (int i = 0; i < 10; i++)
            {
                TransportTask task = new TransportTask(paths[i]);
                TransportTask task2 = new TransportTask(paths[i]);
                Method based_solver = new Method(task, 0);
                Method research_solver = new Method(task2, 1);
                time_relation[i] = (float)(research_solver.elapsed_ticks - based_solver.elapsed_ticks) /
                    based_solver.elapsed_ticks;
                peaks_relation[i] = (float)(research_solver.count_of_peaks - based_solver.count_of_peaks) /
                    based_solver.count_of_peaks;
               
                Console.Write("|{0,13}|", i + 1);
                Console.Write("{0,13}|", task.N);
                Console.Write("{0,13}|{1,13}|{2,13}|{3,13}|{4,13}|{5,13}|",
                    based_solver.elapsed_time, research_solver.elapsed_time, Math.Round(time_relation[i], 4), 
                    based_solver.count_of_peaks, research_solver.count_of_peaks, Math.Round(peaks_relation[i], 4));
                Console.WriteLine();
            }
            Console.WriteLine("|{0, 14}{0, 14}{0, 14}{0, 14}{0, 14}{0, 14}{0, 14}{0, 14}", "|".PadLeft(14, '-'));
            Console.ReadKey();
        }
    }
}