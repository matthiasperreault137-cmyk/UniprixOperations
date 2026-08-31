using System;
using System.Collections.Generic;
using System.Text;
using UniprixOperations.ProductManager;
using UniprixOperations.TaskManagement;
using UniprixOperations.XmlDataManager;
using Task = UniprixOperations.TaskManagement.Task;

namespace UniprixOperations.HelperClasses
{
    public static class TaskSearch
    {
        public static void SortTasksNearest(List<Task> tasks)
        {
            QuickSortTasksNearest(tasks, 0, tasks.Count - 1);
        }

        public static List<Task> GetUnsortedTasksName(string searchQuery)
        {
            List<Task> tasks = new List<Task>();

            if (searchQuery == null || searchQuery.Trim() == "")
            {
                foreach (Task task in DataManager.Instance.TaskHistory.CompletedTasks)
                {
                    tasks.Add(task);
                }
            }
            else
            {
                foreach (Task task in DataManager.Instance.TaskHistory.CompletedTasks)
                {
                    if(task.Name.ToLower().Contains(searchQuery.ToLower()))
                    {
                        tasks.Add(task);
                    }
                }
            }
            return tasks;
        }

        public static List<Task> GetUnsortedTasksUserName(string searchQuery)
        {
            List<Task> tasks = new List<Task>();
            if (searchQuery == null || searchQuery.Trim() == "")
            {
                foreach (Task task in DataManager.Instance.TaskHistory.CompletedTasks)
                {
                    tasks.Add(task);
                }
            }
            else
            {
                foreach (Task task in DataManager.Instance.TaskHistory.CompletedTasks)
                {
                    if (task.CompletionUser.Name != null && task.CompletionUser.Name.ToLower().Contains(searchQuery.ToLower()))
                    {
                        tasks.Add(task);
                    }
                }
            }
            return tasks;
        }

        public static List<Task> GetUnsortedTasksDate(DateOnly? searchQuery)
        {
            List<Task> tasks = new List<Task>();
            if (searchQuery == null)
            {
                foreach (Task task in DataManager.Instance.TaskHistory.CompletedTasks)
                {
                    tasks.Add(task);
                }
            }
            else
            {
                foreach (Task task in DataManager.Instance.TaskHistory.CompletedTasks)
                {
                    if (task.CompletionDate != null && (DateOnly.FromDateTime((DateTime)task.CompletionDate)) == searchQuery)
                    {
                        tasks.Add(task);
                    }
                }
            }
            return tasks;
        }


        public static List<Task> GetUnsortedTasksDateRange(DateOnly? startDate, DateOnly? endDate)
        {
            List<Task> tasks = new List<Task>();
            if (startDate == null && endDate == null)
            {
                foreach (Task task in DataManager.Instance.TaskHistory.CompletedTasks)
                {
                    tasks.Add(task);
                }
            }
            else if (startDate == null)
            {
                foreach (Task task in DataManager.Instance.TaskHistory.CompletedTasks)
                {
                    if (DateOnly.FromDateTime(task.CompletionDate ?? DateTime.Now) <= endDate)
                    {
                        tasks.Add(task);
                    }
                }
            }
            else if (endDate == null)
            {
                foreach (Task task in DataManager.Instance.TaskHistory.CompletedTasks)
                {
                    if (DateOnly.FromDateTime(task.CompletionDate ?? DateTime.Now) >= startDate)
                    {
                        tasks.Add(task);
                    }
                }
            }
            else
            {
                foreach (Task task in DataManager.Instance.TaskHistory.CompletedTasks)
                {
                    if (DateOnly.FromDateTime(task.CompletionDate ?? DateTime.Now) >= startDate && DateOnly.FromDateTime(task.CompletionDate ?? DateTime.Now) <= endDate)
                    {
                        tasks.Add(task);
                    }
                }
            }
            return tasks;
        }



        //Quicksorting by nearest completion date

        private static void QuickSortTasksNearest(List<Task> tasks, int left, int right)
        {
            if (left >= right)
            {
                return;
            }
            else
            {
                int pivotIndex = HoaresPartition(tasks, left, right);

                QuickSortTasksNearest(tasks, left, pivotIndex);
                QuickSortTasksNearest(tasks, pivotIndex + 1, right);
            }
        }
        private static int MedianOfThree(List<Task> tasks, int left, int right)
        {
            int mid = (left + right) / 2;
            DateTime a = (DateTime)tasks[left].CompletionDate;
            DateTime b = (DateTime)tasks[mid].CompletionDate;
            DateTime c = (DateTime)tasks[right].CompletionDate;

            if (a < b)
            {
                if (b < c)
                {
                    return mid;
                }
                else if (a < c)
                {
                    return right;
                }
                else
                {
                    return left;
                }
            }
            else
            {
                if (a < c)
                {
                    return left;
                }
                else if (b < c)
                {
                    return right;
                }
                else
                {
                    return mid;
                }
            }
        }
        private static int HoaresPartition(List<Task> tasks, int left, int right)
        {
            DateTime pivotValue = (DateTime)tasks[MedianOfThree(tasks, left, right)].CompletionDate;
            int i = left - 1;
            int j = right + 1;

            while (true)
            {
                do
                {
                    i++;
                } while (tasks[i].CompletionDate < pivotValue);
                do
                {
                    j--;
                } while (tasks[j].CompletionDate > pivotValue);
                if (i >= j)
                {
                    return j;
                }
                var temp = tasks[i];
                tasks[i] = tasks[j];
                tasks[j] = temp;
            }
        }
    }
}
