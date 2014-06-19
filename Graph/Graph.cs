﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace GraphImplementation
{
    public class Graph
    {
        public delegate void EventChange(Graph sender);
        public event EventChange OnChange;

        public delegate void EventVertexRemoved(Graph sender,Vertex vertex);
        public event EventVertexRemoved OnVertexRemoved;
        public delegate void EventVertexAdded(Graph sender, Vertex vertex);
        public event EventVertexAdded OnVertexAdded;

        public delegate void EventEdgeRemoved(Graph sender, Edge edge);
        public event EventEdgeRemoved OnEdgeRemoved;
        public delegate void EventEdgeAdded(Graph sender, Edge edge);
        public event EventEdgeAdded OnEdgeAdded;

        private List<Vertex> Vertexes = new List<Vertex>();

        /// <summary>
        /// Получить вершину по индексу
        /// </summary>
        /// <param name="index">Индекс вершины</param>
        /// <returns>Возвращает вершину, по её индексу</returns>
        public Vertex GetVertextByIndex(int index)
        {
            try
            {
                return Vertexes[index];
            }
            catch
            {
                throw new Exception("Неправильный индекс вершины");
            }
        }
        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        public Graph()
        {
            
        }
        /// <summary>
        /// Создает новый граф на основе матрици смежности Matrix.
        /// </summary>
        /// <param name="Matrix">Матрица смежности.</param>
        public void GraphFromMatrix(Int32[,] Matrix)
        {
            Clear();
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                AddVertex();
            }
            for(int i = 0;i < Matrix.GetLength(0);i++)
            {
                for (int j = i; j < Matrix.GetLength(1);j++)
                {
                    if (Matrix[i,j]==1)
                    {
                        AddEdge(i, j);
                    }
                }
            }
            
        }
        /// <summary>
        /// Возвращает матрицу смежности графа.
        /// </summary>
        /// <returns>Матрица смежности Int32[,]</returns>
        public Int32[,] GetMatrix()
        {
            Int32[,] Matrix = new Int32[Vertexes.Count,Vertexes.Count];
            Matrix.Initialize();
            foreach(Vertex ver in Vertexes)
            {
                foreach (Vertex inc_ver in ver.Incidented())
                {
                    Matrix[Vertexes.IndexOf(ver), Vertexes.IndexOf(inc_ver)] = 1;
                    Matrix[Vertexes.IndexOf(inc_ver), Vertexes.IndexOf(ver)] = 1;
                }
            }
            return Matrix;
        }
        /// <summary>
        /// Очищает граф, оставляя его пустым.
        /// </summary>
        public void Clear()
        {
            Vertexes.Clear();
        }
        #region "operations with vertexes"
            /// <summary>
            /// Добавляет новую вершину в граф (С данными по умолчанию).
            /// </summary>
            public void AddVertex()
            {
                try
                {
                    Vertex v=new Vertex(Vertexes.Count.ToString());
                    Vertexes.Add(v);
                    if (OnVertexAdded != null) OnVertexAdded(this, v);
                    if (OnChange != null) OnChange(this);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            /// <summary>
            /// Добавляет новую вершину в граф.
            /// </summary>
            /// <param name="Info">Данные для новой вершины.</param>
            public void AddVertex(String Info)
            {
                try
                {
                    Vertex v = new Vertex(Info);
                    Vertexes.Add(v);
                    if (OnVertexAdded != null) OnVertexAdded(this, v);
                    if (OnChange != null) OnChange(this);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            /// <summary>
            /// Добавляет конкретную вершину в граф.
            /// </summary>
            /// <param name="NewVertex">Вершина для добавления.</param>
            public void AddVertex(Vertex NewVertex)
            {
                try
                {
                    if (!Vertexes.Contains(NewVertex)) Vertexes.Add(NewVertex);
                    if (OnVertexAdded != null) OnVertexAdded(this, NewVertex);
                    if (OnChange != null) OnChange(this);
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            /// <summary>
            /// Удаляет вершину из графа.
            /// </summary>
            /// <param name="DelVertex">Вершина, которую требуется удалить.</param>
            public void RemoveVertex(Vertex DelVertex)
            {
                try
                {
                    if (OnVertexRemoved != null) OnVertexRemoved(this, DelVertex);
                    foreach (Vertex Incident in DelVertex.Incidented())
                    {
                        if (OnEdgeRemoved != null) OnEdgeRemoved(this, new Edge(Incident,DelVertex));
                        Incident.Vertexes.Remove(DelVertex);
                       
                    }
                    Vertexes.Remove(DelVertex);
                    if (OnChange != null) OnChange(this);
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            /// <summary>
            /// Удаляет вершину из графа по указанному индексу
            /// </summary>
            /// <param name="DelIndex">Индекс вершины, которую требуется удалить.</param>
            public void RemoveVertex(Int32 DelIndex)
            {
                try
                {
                    foreach (Vertex Incident in Vertexes[DelIndex].Incidented())
                    {
                        if (OnEdgeRemoved != null) OnEdgeRemoved(this, new Edge(Incident, Vertexes[DelIndex]));
                        Incident.Vertexes.Remove(Vertexes[DelIndex]);
                    }
                    if (OnVertexRemoved != null) OnVertexRemoved(this, Vertexes[DelIndex]);
                    Vertexes.Remove(Vertexes[DelIndex]);
                    if (OnChange != null) OnChange(this);
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        #endregion
        #region "operation with edges"

            /// <summary>
            /// Добавляет ребро в граф.
            /// </summary>
            /// <param name="Vertex1">Первая вершина, которая входит в добавляемое ребро.</param>
            /// <param name="Vertex2">Вторая вершина, которая входит в добавляемое ребро.</param>
            public void AddEdge(Vertex Vertex1, Vertex Vertex2)
            {
                try
                {
                    if (Vertex1 != Vertex2)
                    {
                        if (!Vertex1.Vertexes.Contains(Vertex2)) Vertex1.Vertexes.Add(Vertex2);
                        if (!Vertex2.Vertexes.Contains(Vertex1)) Vertex2.Vertexes.Add(Vertex1);
                        if (OnEdgeAdded != null) OnEdgeAdded(this, new Edge(Vertex1, Vertex2));
                        if (OnChange != null) OnChange(this); 
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            /// <summary>
            /// Добавляет ребро в граф, на основе индексов вершин.
            /// </summary>
            /// <param name="Index1">Индекс первой вершины, которая входит в добавляемое ребро.</param>
            /// <param name="Index2">Индекс второй вершины, которая входит в добавляемое ребро</param>
            public void AddEdge(Int32 Index1, Int32 Index2)
            {
                Vertex v;
                if (Index1 != Index2)
                {
                    try
                    {
                        v = Vertexes[Index1];
                        v = Vertexes[Index2];
                    }
                    catch
                    {
                        throw new Exception("Индекс кривой!");
                    }
                    if (!Vertexes[Index1].Vertexes.Contains(Vertexes[Index2])) Vertexes[Index1].Vertexes.Add(Vertexes[Index2]);
                    if (!Vertexes[Index2].Vertexes.Contains(Vertexes[Index1])) Vertexes[Index2].Vertexes.Add(Vertexes[Index1]);
                    if (OnEdgeAdded != null) OnEdgeAdded(this, new Edge(Vertexes[Index1], Vertexes[Index2]));
                    if (OnChange != null) OnChange(this);
                }
            }
            /// <summary>
            /// Удаляет ребро соединяющие вершины Vertex1 и Vertex2, если таковые имеется.
            /// </summary>
            /// <param name="Vertex1">Первая вершина.</param>
            /// <param name="Vertex2">Вторая вершина.</param>
            public void RemoveEdge(Vertex Vertex1, Vertex Vertex2)
            {
                try
                {
                    if (OnEdgeRemoved != null) OnEdgeRemoved(this,new Edge(Vertex1,Vertex2));
                    if (Vertex1.Vertexes.Contains(Vertex2)) Vertex1.Vertexes.Remove(Vertex2);
                    if (Vertex2.Vertexes.Contains(Vertex1)) Vertex2.Vertexes.Remove(Vertex1);
                    if (OnChange != null) OnChange(this);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            /// <summary>
            /// Удаляет ребро соединяющие вершины с индексами Index1 и Index2, если таковые имеется.
            /// </summary>
            /// <param name="Index1">Индекс первой вершины.</param>
            /// <param name="Index2">Индекс второй вершины</param>
            public void RemoveEdge(Int32 Index1, Int32 Index2)
            {
                Vertex v;
                if (Index1 != Index2)
                {
                    try
                    {
                        v = Vertexes[Index1];
                        v = Vertexes[Index2];
                    }
                    catch
                    {
                        throw new Exception("Индекс кривой!");
                    }
                    if (OnEdgeRemoved != null) OnEdgeRemoved(this, new Edge(Vertexes[Index1], Vertexes[Index2]));
                    if (Vertexes[Index1].Vertexes.Contains(Vertexes[Index2])) Vertexes[Index1].Vertexes.Remove(Vertexes[Index2]);
                    if (Vertexes[Index2].Vertexes.Contains(Vertexes[Index1])) Vertexes[Index2].Vertexes.Remove(Vertexes[Index1]);
                    if (OnChange != null) OnChange(this);
                }
            }
        #endregion
        /// <summary>
        /// Пустота графа
        /// </summary>
        /// <returns>Возвращает истину, если граф пуст</returns>
        [Description("Показывает является ли граф пустым")]
        public Boolean IsEmpty
        {
            get { return Vertexes.Count == 0; }
        }
        /// <summary>
        /// Полнота графа
        /// </summary>
        /// <returns>Возвращает истину, если граф полный</returns>
        [Description("Показывает является ли граф полным")]
        public Boolean IsFull
        {
            get 
            { 
                int[,] matrix = this.GetMatrix();
                int n = matrix.GetLength(0);
                bool flag=true;
                for(int i=0;i<n;i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if(i!=j && matrix[i,j]==0)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                return flag;
            }
        }
        /// <summary>
        /// Проверка на древестность
        /// </summary>
        /// <returns>Возвращает истину, если граф дерево.</returns>
        [Description("Показывает является ли граф деревом")]
        public Boolean IsTree
        {
            get
            {
                if (this.IsConnected)
                {
                    int[,] matrix = this.GetMatrix();
                    int n = matrix.GetLength(0);
                    bool[] flag = new bool[n];
                    for (int j = 0; j < n; j++)
                        flag[j] = true;
                    int i = 0;
                    int count = 0;
                    while (i < n)
                    {
                        count = 0;
                        for (int j = 0; j < n; j++)
                        {
                            if (matrix[i, j] == 1 && flag[i] && flag[j])
                            {
                                count++;
                            }
                        }
                        if (count == 1)
                        {
                            flag[i] = false;
                            i = 0;
                        }
                        i++;
                    }
                    count = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (flag[j]) count++;
                    }
                    return (count == 1);
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Проверяет граф на связность
        /// </summary>
        /// <returns>Возвращает истину, если граф связанный.</returns>
        [Description("Показывает является ли граф связанным")]
        public Boolean IsConnected
        {
            get
            {
                int[,] matrix = this.GetMatrix();
                bool flag = true;
                bool inColumn;
                int n = matrix.GetLength(0);
                if (n == 1) return true;
                for (int i = 0; i < n; i++)
                {
                    inColumn = false;
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] != 0)
                        {
                            inColumn = true;
                            break;
                        }
                    }
                    if (!inColumn)
                    {
                        flag = false;
                        break;
                    }
                }

                return flag;
            }
        }
        /// <summary>
        /// Проверка на содержание Эйлерового цикла
        /// </summary>
        /// <returns>Возвращает истину, если граф содержит Эйлеров цикл.</returns>
        [Description("Показывает является ли граф Эйлеровым")]
        public Boolean IsEuler
        {
            get
            {
                bool flag = true;
                if (IsConnected)
                {
                    foreach (Vertex i in Simple())
                    {
                        if (i.Degree % 2 != 0)
                        {
                            flag = false;
                            break;
                        }
                    }
                    return flag;
                }
                else
                    return false;
            }
        }
        /// <summary>
        /// Проверка на лес
        /// </summary>
        /// <returns>Возвращает является ли граф лесом</returns>
        [Description("Показывает является ли граф лесом")]
        public Boolean IsWood
        {
            get
            {
                bool flag = true;
                foreach (Graph g in GetComponents())
                {
                    if (!g.IsTree)
                    {
                        flag = false;
                        break;
                    }
                }
                return flag;
            }
        }
        /// <summary>
        /// Проверка на двудольность
        /// </summary>
        /// <returns>Возвращает является ли граф двудольным</returns>
        [Description("Показывает является ли граф двудольным")]
        public Boolean IsBigraph
        {
            get
            {
                bool flag = true;
                Dictionary<Vertex, int> Mark = new Dictionary<Vertex, int>();
                foreach (Vertex v in BFS())
                {
                    if (!Mark.ContainsKey(v))
                    {
                        Mark.Add(v, 1);
                    }
                    foreach (Vertex i in v.Vertexes)
                    {
                        if (!Mark.ContainsKey(i))
                        {
                            Mark.Add(i, -Mark[v]);
                        }
                        else
                        {
                            if (Mark[i] != -Mark[v])
                                flag = false;
                        }
                    }
                }
                return flag;
            }
        }
        /// <summary>
        /// Разбиение на комоненты связности
        /// </summary>
        /// <returns>Возвращает список графов</returns>
        public List<Graph> GetComponents()
        {
            List<Graph> listG= new List<Graph>();
            List<Vertex> iteratorList = new List<Vertex>();
            List<Vertex> vertexes = new List<Vertex>();
            if (Vertexes.Count != 0)
            {
                foreach (Vertex i in this.Vertexes)
                {
                    if (!iteratorList.Contains(i))
                    {
                        vertexes.Clear();
                        DFScompRec(i, iteratorList,vertexes);
                        listG.Add(new Graph());
                        foreach(Vertex v in vertexes)
                        {
                            listG[listG.Count - 1].AddVertex(v);
                        }
                    }
                }
            }
            return listG;
        }
        private void DFScompRec(Vertex v, List<Vertex> list,List<Vertex> vertexes)
        {
            if (!list.Contains(v))
            {
                list.Add(v);
                vertexes.Add(v);
                DFScompRec(v, list, vertexes);
            }
            foreach (Vertex i in v.Incidented())
            {
                if (!list.Contains(i))
                {
                    list.Add(i);
                    vertexes.Add(i);
                    DFScompRec(i, list,vertexes);

                }
            }
        }
        #region "iterators"

            /// <summary>
            /// Обход графа по ребрам.
            /// </summary>
            /// <returns>Возвращает все ребра графа.</returns>
            public IEnumerable<Edge> Edges()
            {
                int[,] matrix = this.GetMatrix();
                int n=matrix.GetLength(0);
                for(int i=0;i<n;i++)
                {
                    for (int j = i; j < n; j++)
                    {
                        if(matrix[i,j]!=0)
                        {
                            yield return new Edge(Vertexes[i], Vertexes[j]);
                        }
                    }
                }
            }
            /// <summary>
            /// Обход графа в глубину.
            /// </summary>
            /// <returns>Возвращает вершины в порядке обхода.</returns>
            public IEnumerable<Vertex> DFS()
            {
                List<Vertex> iteratorList = new List<Vertex>();

                if (Vertexes.Count != 0)
                {
                    foreach (Vertex i in this.Vertexes)
                    {
                        if (!iteratorList.Contains(i))
                        {
                            DFSrec(i, iteratorList);
                        }
                    }
                    foreach (Vertex i in iteratorList)
                    {
                        yield return i;
                    }
                }
            }
            private void DFSrec(Vertex v,List<Vertex> list)
            {
                if (!list.Contains(v))
                {
                    list.Add(v);
                    DFSrec(v, list);
                }
                foreach(Vertex i in v.Incidented())
                {
                    if (!list.Contains(i))
                    {  
                        list.Add(i);
                        DFSrec(i,list);
                        
                    }
                }
            }


            /// <summary>
            /// Обход графа в ширину.
            /// </summary>
            /// <returns>Возвращает вершины в порядке обхода.</returns>
            public IEnumerable<Vertex> BFS()
            {
                List<Vertex> iteratorList = new List<Vertex>();
   
                if (Vertexes.Count != 0)
                {
                    BFSrec(Vertexes[0], iteratorList);
                    foreach (Vertex i in iteratorList)
                    {
                        yield return i;
                    }
                }
            }
            private void BFSrec(Vertex v, List<Vertex> list)
            {
                
                Queue<Vertex> queue= new Queue<Vertex>();
              
                queue.Enqueue(v);
                list.Add(v);
 
                foreach (Vertex i in this.Vertexes)
                {
                    if (!list.Contains(i))
                    {
                        queue.Enqueue(i);
                        list.Add(i);
                    }
                    while (queue.Count != 0)
                    {
                      
                        foreach (Vertex item in queue.Peek().Incidented())
                        {
                            if (!list.Contains(item))
                            {
                                list.Add(item);
                                queue.Enqueue(item);
                            }
                        }
                        queue.Dequeue();
                        
                    }
                }
            }
            
            /// <summary>
            /// Перечисляет все вершины инцидентные данной.
            /// </summary>
            /// <param name="CurrentVertex">Вершина.</param>
            /// <returns>Возвращает все инцидентные вершины в произвольном порядке.</returns>
            public IEnumerable<Vertex> Incidented(Vertex CurrentVertex)
            {
                foreach (Vertex ver in CurrentVertex.Vertexes)
                {
                    yield return ver;
                }
            }


            /// <summary>
            /// Перечисляет все вершины графа в произвольном порядке.
            /// </summary>
            /// <returns>Возвращает все вершины в произвольном порядке.</returns>
            public IEnumerable<Vertex> Simple()
            {
                foreach (Vertex ver in Vertexes)
                {
                    yield return ver;
                }
            }

        #endregion
    }
}
