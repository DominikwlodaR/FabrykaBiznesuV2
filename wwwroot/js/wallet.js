// rysowanie wykresu
google.charts.load("current", { packages: ["corechart"] });
google.charts.setOnLoadCallback(drawChart);
function drawChart() {
    
    
    var data = google.visualization.arrayToDataTable([
        ['Budget', 'Hours per Day'],
        ['Wolny budżet', events.data[0] - events.data[1] - events.data[2]],
        ['Wykorzytsany budzet', events.data[1]],
        ['Niezatwierdzone wydatki', events.data[2]],
       
    ]);

   
    var options = {
        width: '100%',
        height:'100%',
        pieHole: 0.4,
        legend: 'none',
        chartArea: {
            left: '5%', 
            top: '5%', 
            width: '100%', 
            height: '90%' 
        },
    
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart'));
    chart.draw(data, options);
    window.addEventListener('resize', function () {
        chart.draw(data, options);
    });
}

//obsługa filrów
var filterForm = document.getElementById("bugdet-filters")
var filter = document.getElementById('filters')
filter.addEventListener("click", function () {
    filterForm.style.display = 'block';
});

var cancelFilter = document.getElementById('FilterReset')
cancelFilter.addEventListener("click", function () {
    filterForm.style.display = 'none';

});

//walidacja formularza dodawania
function formValidate() {
    var description = document.getElementById('BAdd_description').value
    var sum = document.getElementById('BAdd_sum').value
    var descAlert = document.getElementById('descAlert')
    var sumAlert = document.getElementById('sumAlert')
    if (description.trim() == 0) {
        descAlert.innerText = "Proszę podać opis"
        return false;
    } else if(sum.trim() == 0) {
        sumAlert.innerText = "Proszę podać Kwote"
        return false;
    }
    return true;

}
//sortowanie Tabeli wydatków
function sortTable(n) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("budgetTable");
    switching = true;
    dir = "asc";
    while (switching) {
        switching = false;
        rows = table.rows;
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];
            if (dir == "asc") {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            } else if (dir == "desc") {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchcount++;
        } else {
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
    updateSortIcons(n, dir);
}
// zachowanie ikon przy sortowaniu
function updateSortIcons(column, direction) {
    var arrowIcons = document.querySelectorAll(".arrow");
    arrowIcons.forEach(function (icon) {
        icon.remove();
    });
    var th = document.querySelector("#budgetTable thead tr th:nth-child(" + (column + 1) + ")");
    var arrowIcon = document.createElement("i");
    arrowIcon.className = direction === "asc" ? "bi bi-arrow-up arrow" : "bi bi-arrow-down arrow";
    th.appendChild(arrowIcon);
}

//filtry tabeli
function filterTable() {    
    var inputs = document.getElementsByClassName('filterInput');
    var table = document.getElementById('budgetTable');
    var rows = table.getElementsByTagName('tr');
    var noDataMessageRow = document.getElementById('noDataMessageRow');
    var hasVisibleRows = false;


    for (var i = 1; i < rows.length; i++) { 
        var row = rows[i];
        var display = true;

        for (var j = 0; j < inputs.length; j++) {
            var filterInput = inputs[j];
            var columnIndex = parseInt(filterInput.getAttribute('data-column'));
            var filterValue = filterInput.value.toLowerCase();

            var td = row.getElementsByTagName('td')[columnIndex];
            if (td) {
                var cellValue = td.textContent || td.innerText;



                if (cellValue.toLowerCase().indexOf(filterValue) === -1) {
                    display = false;
                    break;
                }
            }
        }

        if (display) {
            row.style.display = '';
            hasVisibleRows = true;
        } else {
            row.style.display = 'none';
        }
    }


    if (!hasVisibleRows) {
        noDataMessageRow.style.display = 'table-row';
    } else {
        noDataMessageRow.style.display = 'none';
    }
}

var filterInputs = document.getElementsByClassName('filterInput');
for (var k = 0; k < filterInputs.length; k++) {
    filterInputs[k].addEventListener('input', filterTable);
}


function clearFilters() {
    var inputs = document.getElementsByClassName('filterInput');
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].value = '';
    }
    filterTable();
}

var clear = document.getElementById("FilterClear")

clear.addEventListener('click', clearFilters);



