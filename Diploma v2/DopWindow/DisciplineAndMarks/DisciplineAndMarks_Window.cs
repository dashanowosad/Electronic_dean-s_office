using System.Windows;
using System.Windows.Controls;

namespace Diploma_v2.DopWindow.DisciplineAndMarks
{
    public class DisciplineAndMarks_Window
    {
        
        private MarkDB _markDb;
        public DisciplineAndMarks_Window(string StudDb)
        {
            this._markDb = new MarkDB(StudDb);
        }

        public void Insert_New_Discipline_Or_Update(Window MainWindow, int idStud, DataGrid markTable, int idDiscipline = -1)
        {
            DisciplineAddOrUpdate_Window disciplineAddOrUpdateWindow = new DisciplineAddOrUpdate_Window(idStud, idDiscipline);
            disciplineAddOrUpdateWindow.Owner = MainWindow;
            if (idDiscipline == -1)
                disciplineAddOrUpdateWindow.Title = "Добавление нового предмета/оценки";
            else
                disciplineAddOrUpdateWindow.Title = "Редактирование предмета/оценки";
            disciplineAddOrUpdateWindow.Show();
            
            disciplineAddOrUpdateWindow.Closed += (s, eventarg) =>
            {
                this._markDb.LoadMark(markTable, idStud.ToString());
            };
        }
    }
}