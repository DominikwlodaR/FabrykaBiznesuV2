
let resources = [];
for ( i = 0; i < teams.length; i++) {
    let item = { id: teams[i].TeamId, title: teams[i].TeamName }
    resources.push(item)
}

let events = []
let name = ""
for (i = 0; i < tasks.length; i++) {
    var startDate = tasks[i].StartDate
    var formatedStartDate = startDate.split("T")
    for (y = 0; y < teams.length; y++) {
        if (tasks[i].TeamId == teams[y].TeamId) {
            name = teams[y].TeamName + ": " + tasks[i].TaskName
        }
    }
    var endaDate = tasks[i].EndDate
    var formatedEndDate = endaDate.split("T")
    let item = { id: tasks[i].Id, resourceId: tasks[i].TeamId, start: formatedStartDate[0], end: formatedEndDate[0], title: name }
    events.push(item)
}


document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    
    var today = new Date(); 
    var calendar = new FullCalendar.Calendar(calendarEl, {
        schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',
        initialView: 'resourceTimelineWeek', 
   
        firstDay: 1,
        slotDuration: { days: 1 }, 
        scrollTime: '07:00:00',
        slotLabelFormat: { weekday: 'short', month: 'numeric', day: 'numeric' }, 
        locale: 'pl',
        nowIndicator: true,
        headerToolbar: {
            left: 'today prev,next',
            center: 'title',
            right: '',
        },
        resourceAreaColumns: [
            {
                field: 'title',
                headerContent: 'Zaespoły'
            },
        ],
        resources: resources,
        events: events,
        buttonText: {
            today: 'Dzisiaj'
        },

        
    });

    calendar.render();
    var calendarWrapper = document.querySelector('.fc-scroller');
    var scrollbar = document.createElement('div');
    scrollbar.className = 'scrollbar';
    calendarWrapper.appendChild(scrollbar);
  });