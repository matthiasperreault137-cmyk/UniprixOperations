using UniprixOperations.ProductManager;
using UniprixOperations.TaskManagement;
using UniprixOperations.XmlDataManager;


namespace UniprixOperations.HelperClasses
{
    public static class RecurringTaskSearch
    {
        public static RecurringTask SearchId(int id)
        {
            foreach (RecurringTask task in DataManager.Instance.RecurringTasks.RecurringTaskList)
            {
                if (task.Id == id)
                {
                    return task;
                }
            }
            return null;
        }

        public static DateTime? FindNearestSubtask(RecurringTask task)
        {
            DateTime? nearestDate = null;
            foreach (SubTask subtask in task.SubTasks)
            {
                if (subtask.CreationDate < nearestDate || nearestDate == null)
                {
                    nearestDate = subtask.CreationDate;
                }
            }
            return nearestDate;
        }


        public static void SortRecurringTasksNearest(List<RecurringTask> tasks)
        {
            QuickSortRecurringTasksNearest(tasks, 0, tasks.Count - 1);
        }


        public static void SortSubtasksNearest(List<SubTask> subtasks)
        {
            QuickSortSubtasksNearest(subtasks, 0, subtasks.Count - 1);
        }

        //QuickSort Recurring Tasks

        private static void QuickSortRecurringTasksNearest(List<RecurringTask> tasks, int left, int right)
        {
            if (left >= right)
            {
                return;
            }
            else
            {
                int pivotIndex = HoaresPartition(tasks, left, right);

                QuickSortRecurringTasksNearest(tasks, left, pivotIndex);
                QuickSortRecurringTasksNearest(tasks, pivotIndex + 1, right);
            }
        }
        private static int MedianOfThree(List<RecurringTask> tasks, int left, int right)
        {
            int mid = (left + right) / 2;
            DateTime first = tasks[left].CreationDate;
            DateTime second = tasks[mid].CreationDate;
            DateTime third = tasks[right].CreationDate;

            float a = (DateTime.Today - first).Days / (float)tasks[left].Recurrence;
            float b = (DateTime.Today - second).Days / (float)tasks[mid].Recurrence;
            float c = (DateTime.Today - third).Days / (float)tasks[right].Recurrence;


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
        private static int HoaresPartition(List<RecurringTask> tasks, int left, int right)
        {
            float pivotValue = (DateTime.Today - tasks[MedianOfThree(tasks, left, right)].CreationDate).Days / (float)tasks[MedianOfThree(tasks, left, right)].Recurrence;
            int i = left - 1;
            int j = right + 1;

            while (true)
            {
                do
                {
                    i++;
                } while ((DateTime.Today - tasks[i].CreationDate).Days / (float)tasks[i].Recurrence > pivotValue);
                do
                {
                    j--;
                } while ((DateTime.Today - tasks[j].CreationDate).Days / (float)tasks[j].Recurrence < pivotValue);
                if (i >= j)
                {
                    return j;
                }
                var temp = tasks[i];
                tasks[i] = tasks[j];
                tasks[j] = temp;
            }
        }



        //QuickSort Subtasks

        private static void QuickSortSubtasksNearest(List<SubTask> subtasks, int left, int right)
        {
            if (left >= right)
            {
                return;
            }
            else
            {
                int pivotIndex = HoaresPartitionSubtask(subtasks, left, right);

                QuickSortSubtasksNearest(subtasks, left, pivotIndex);
                QuickSortSubtasksNearest(subtasks, pivotIndex + 1, right);
            }
        }
        private static int MedianOfThreeSubtask(List<SubTask> subtasks, int left, int right)
        {
            int mid = (left + right) / 2;
            DateTime first = subtasks[left].CreationDate;
            DateTime second = subtasks[mid].CreationDate;
            DateTime third = subtasks[right].CreationDate;

            float a = (DateTime.Today - first).Days / (float) subtasks[left].Recurrence;
            float b = (DateTime.Today - second).Days / (float) subtasks[mid].Recurrence;
            float c = (DateTime.Today - third).Days / (float) subtasks[right].Recurrence;

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
        private static int HoaresPartitionSubtask(List<SubTask> subtasks, int left, int right)
        {
            float pivotValue = (DateTime.Today - subtasks[MedianOfThreeSubtask(subtasks, left, right)].CreationDate).Days / (float)subtasks[MedianOfThreeSubtask(subtasks, left, right)].Recurrence;
            int i = left - 1;
            int j = right + 1;

            while (true)
            {
                do
                {
                    i++;
                } while ((DateTime.Today - subtasks[i].CreationDate).Days / (float)subtasks[i].Recurrence > pivotValue);
                do
                {
                    j--;
                } while ((DateTime.Today - subtasks[j].CreationDate).Days / (float)subtasks[j].Recurrence < pivotValue);
                if (i >= j)
                {
                    return j;
                }
                var temp = subtasks[i];
                subtasks[i] = subtasks[j];
                subtasks[j] = temp;
            }
        }
    }
}
