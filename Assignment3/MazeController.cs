using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    public class MazeController
    {
        public ISubject subject;
        public List<IObserver> observers;

        MazeController()
        {

        }

        public MazeController(ISubject subject)
        {
            this.subject = subject;
        }

        public void AddObserver(IObserver observer)
        {
            this.subject.AttachObserver(observer);
        }

        public void GenerateMaze()
        {
            this.subject.GenerateMaze();
        }
    }
}
