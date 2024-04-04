$(document).ready(function () {

    
    $(".hoverElement").hover(
        function () {
            $(this).find(".deleteRelayBnt").fadeIn();
        },
        function () {
            $(this).find(".deleteRelayBnt").fadeOut();
        }
    );


    $(".relayBnt").click(function () {
        var commentId = $(this).data("comment-id");
        $(".reply-form[data-comment-id=" + commentId + "]").toggle();
            
    });

    $(".deleteRelayBnt").click(function (e) {
        var commentId = $(this).data("comment-id");
        $('#deleCommConfirm').modal('show');
        $('#deleCommConfirm .close').on('click', function () {
            $('#deleCommConfirm').modal('hide');
        });
        $('#deleCommConfirm #cancelCommDelete').on('click', function () {
            $('#deleCommConfirm').modal('hide');
        });

        $('#deleCommConfirm #confirmCommDelete').on('click', function () {
            var url = '/Agenda/DeleteComm/' + commentId
            console.log(url)
            $.ajax({
                url: url,
                type: 'GET',

                success: function (response) {

                    console.log('Usunięto pomyślnie');
                    location.reload();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error('Błąd podczas usuwania', jqXHR, textStatus, errorThrown);

                    console.log('Status: ' + jqXHR.status);
                    console.log('Status Text: ' + jqXHR.statusText);
                    console.log('Response Text: ' + jqXHR.responseText);
                }
            });
         });

    });
    $(".addRelayBnt").click(function (e) {
        e.preventDefault();
        var commentForm = $(this).closest(".reply-form");
        var userId = $(this).closest(".reply-form").find("input[name='relayUserId']").val();
        var userName = $(this).closest(".reply-form").find("input[name='relayUserName']").val();
        var projectId = $(this).closest(".reply-form").find("input[name='relayProjectId']").val();
        var parentId = $(this).closest(".reply-form").find("input[name='parrentCommId']").val();
        var date = $(this).closest(".reply-form").find("input[name='commDate']").val();
        var content = $(this).closest(".reply-form").find("textarea[name='content']").val();

        var formdata = {
            UserId: userId,
            UserName: userName,
            ProjectId: projectId,
            Comment: content,
            Date: date,
            isReply: true,
            ParentId: parentId
        };
        console.log(formdata)

        $.ajax({
            type: 'POST',
            url: '/Agenda/AddRelay',
            data: formdata,
            success: function (data) {
                console.log(data);
                location.reload();

            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("Request failed: " + textStatus, errorThrown);

            }
        })


        //commentForm.hide();
        //commentForm.find("textarea[name='content']").val("");
    });

    $('#commForm').submit(function (e) {
        e.preventDefault(); 
        var formdata = {
            UserId: $('#commUserId').val() ,
            UserName: $('#commUserName').val() ,
            ProjectId: $('#commProjectId').val() ,
            Comment: $('#commText').val(),
            Date: $('#commDate').val(),
            isReply: false
        };
        console.log(formdata)

        $.ajax({
            type: 'POST',
            url: '/Agenda/AddComm',
            data: formdata,
            success: function (data) {
                console.log(data);
                location.reload();
             
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("Request failed: " + textStatus, errorThrown);

            }
        })
    });
$('#taskTeam').val(),

    


    $('#taskForm').submit(function (e) {
        e.preventDefault(); 
    
        var checkbox = document.getElementById('relationChechbox')
        var predecessor
        if (checkbox.checked) {
            predecessor = $('#predecessor').val()
        }else{
            predecessor = null
        }
   
        var formData = {
            TaskId: $('#formTaskNumber').val(),
            TaskName: $('#formTaskName').val(),
            StartDate: $('#formTaskStartDate').val(),
            EndDate: $('#formTaskEndDate').val(),
            ProjectID: $('#formTaskprojID').val(),
            Id: $('#formTaskId').val(),
            isSubtask: $('#isSubtask').val(),
            Predecessor: predecessor,
            TeamId: $('#taskTeam').val(),
            Status: $('#status').val(),
            Description: $('#description').val()
        };

        var action = $(document.activeElement).attr('id');
        console.log(action)
        var url;
        if (action === 'addButton') {
            url = '/Agenda/AddTask';
        } else if (action === 'editButton') {
            url = '/Agenda/EditTask';
        }

        $.ajax({
            type: 'POST',
            url: url, 
            data: formData,
            success: function (data) {

                console.log(data);

                $('#taskForm')[0].reset();
                location.reload();
                $('#TaskModal').removeClass('show');
                $('.modal-backdrop').remove();
                $('body').removeClass('modal-open');
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("Request failed: " + textStatus, errorThrown);
              
            }
        });
    });

});


function contextMenuOpen(args) {
    var record = args.rowData;
    if (!record) {
        args.hideItems = args.hideItems || [];

        args.hideItems.push("Edytuj");
        args.hideItems.push("Dodaj podzadanie");
        args.hideItems.push("Szybkie dodawnie poniżej");
        args.hideItems.push("Przenieś wyżej");
        args.hideItems.push("Przenieś niżej");
        args.hideItems.push("Zamień na kamień milowy");
        args.hideItems.push("Usuń");
      



    } else {
        if (record.SubTasks != null) {
            args.hideItems.push("Zamień na kamień milowy");
        }

        args.hideItems.push("Dodaj");
       
    }


}


function contextMenuClick(args) {
    var ganttObj = document.getElementById("Gantt").ej2_instances[0];
    var record = args.rowData;
    if (args.item.id === 'editTask') {
        $('#TaskModal').modal('show');
       
        var formattedStartDate = record.StartDate.toISOString().split('T')[0];
        var formattedEndDate = record.EndDate.toISOString().split('T')[0];
       
        $('#formTaskNumber').val(record.TaskId);
        
        $('#formTaskName').val(record.TaskName);
        $('#formTaskStartDate').val(formattedStartDate);
        $('#formTaskEndDate').val(formattedEndDate);
        $('#formTaskId').val(record.Id);
        $('#isSubtask').val(record.isSubtask);
        $('#predecessor').val(record.Predecessor);
        $('#taskStatus').val(record.Status);
        $('#taskTeam').val(record.TeamId);
        $('#description').val(record.Description);
        $('#status').val(record.Status);
     
        var relationType = document.getElementById('relationType')
        var relationSelect = document.getElementById('relationSelect')
        var checbox = document.getElementById('relationChechbox')


        if (record.Predecessor != null) {
            checbox.checked = true;
            var type = record.Predecessor.slice(-2)
            var task = record.Predecessor.slice(0,-2)
           
            relationType.value = type
            relationType.disabled = false
            relationSelect.value = task
            console.log(task)
            relationSelect.disabled = false

        }
        var addButton = document.getElementById('addButton')
        var editButton = document.getElementById('editButton')

        addButton.style.display= 'none'
        editButton.style.display = 'block'
        console.log("widzisz mnie")
        var addTaskFormHeader = document.getElementById('addTaskFormHeader')
        var editTaskFormHeader = document.getElementById('editTaskFormHeader')
        addTaskFormHeader.style.display = 'none'
        editTaskFormHeader.style.display = 'block'
        $('#TaskModal').on('hidden.bs.modal', function () {

            document.getElementById('taskForm').reset();
            relationType.disabled = true
            relationSelect.disabled = true
        });

        $('#TaskModal .close').on('click', function () {
            $('#TaskModal').modal('hide');
        });

    }
    if (args.item.id == 'deleteTask') {
        $('#deleTaskConfirm').modal('show');
        $('#deleTaskConfirm .close').on('click', function () {
            $('#deleTaskConfirm').modal('hide');
        });
        var idToDelete = record.Id;
        $('#deleTaskConfirm #confirmDelete').on('click', function () {
            var url = '/Agenda/DeleteTask/' + idToDelete
            $.ajax({
                url: url,
                type: 'GET',

                success: function (response) {

                    console.log('Usunięto pomyślnie');
                    location.reload();
                },
                error: function (error) {

                    console.error('Błąd podczas usuwania', error);
                }
            });


            $('#deleTaskConfirm').modal('hide');
           
        });

        $('#deleTaskConfirm #cancelDelete').on('click', function () {
            $('#deleTaskConfirm').modal('hide');
        })
    }
    if (args.item.id == 'addBelow') {
        var currentDate = new Date();
        var formattedDate = currentDate.toISOString().split('T')[0];
        var newID
        if (record.SubTasks !== undefined && record.SubTasks !== null) {
           
            newID = record.TaskId + record.SubTasks.length + 1
        }else{
            newID = record.TaskId + 1
        }

        var formData = {
            TaskId: newID,
            TaskName: "Nowe zadanie",
          
            ProjectID: record.ProjectID
        }
        console.log(record.ProjectID)
        $.ajax({
            type: 'POST',
            url: '/Agenda/AddTaskBelow',
            data: formData,
            success: function (data) {
                location.reload();
                console.log(data);
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("Request failed: " + textStatus, errorThrown);
                console.log(xhr.responseText);
            }
        });
      
    }
    if (args.item.id == 'addSubTask') {
        var idToDelete = record.Id;
        var url = '/Agenda/AddSubTask/' + idToDelete
        $.ajax({
            url: url,
            type: 'GET',

            success: function (response) {

                console.log('Usunięto pomyślnie');
                location.reload();
            },
            error: function (error) {

                console.error('Błąd podczas dodania subtaska', error);
            }
        });

    }
    if (args.item.id == 'mileStone') {

        
        let recordDate = new Date(record.StartDate)
        let year = recordDate.getFullYear();
        let month = ('0' + (recordDate.getMonth() + 1)).slice(-2);
        let day = ('0' + recordDate.getDate()).slice(-2);
        let newDate = `${year}-${month}-${day}`

        let date = new Date(newDate)
        date.setDate(date.getDate() - 1)
        year = date.getFullYear();
        month = ('0' + (date.getMonth() + 1)).slice(-2);
        day = ('0' + date.getDate()).slice(-2);
        let newEndDate = `${year}-${month}-${day}`
        var taskData = {
            TaskId: record.TaskId,
            TaskName: record.TaskName,
            StartDate: newDate,
            EndDate: newEndDate,
            ProjectID: record.ProjectID,
            Id: record.Id,
            isSubtask: record.isSubtask,
            Predecessor: record.predecessor,
            TeamId: record.TeamId,
            Status: record.Status

        };
     
        $.ajax({
            type: 'POST',
            url: '/Agenda/EditTask',
            data: taskData,
            success: function (data) {
                console.log(data);
                location.reload();
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("Request failed: " + textStatus, errorThrown);

            }
        });
        
        
    }
   
 }
        
   
    


var openAddTaskForm = document.getElementById('openAddTaskForm');
openAddTaskForm.addEventListener("click", function () {
    var addButton = document.getElementById('addButton')
    var editButton = document.getElementById('editButton')

    addButton.style.display = 'block'
    editButton.style.display = 'none'

    var addTaskFormHeader = document.getElementById('addTaskFormHeader')
    var editTaskFormHeader = document.getElementById('editTaskFormHeader')
    addTaskFormHeader.style.display = 'block'
    editTaskFormHeader.style.display = 'none'
});


$('#TaskModal').on('hidden.bs.modal', function () {
    document.getElementById('taskForm').reset();
});


document.addEventListener('DOMContentLoaded', function () {
    var ganttObj = document.getElementById('Gantt').ej2_instances[0];

    document.getElementById('expandAll').addEventListener('click', function () {
        ganttObj.expandAll();
    });

    document.getElementById('collapseAll').addEventListener('click', function () {
        ganttObj.collapseAll();
    });
    

});

var relationChechbox = document.getElementById('relationChechbox')
var relationSelect = document.getElementById('relationSelect')
var relationType = document.getElementById('relationType')
var predecessor = document.getElementById('predecessor')

function relationChecked() {
   
    if (relationChechbox.checked) {
        relationSelect.disabled = false;
        relationType.disabled = false;
        
    }

  
}

relationSelect.addEventListener('change', () => {
    predecessor.value = relationSelect.value + relationType.value
});

relationType.addEventListener('change', () => {
    predecessor.value = relationSelect.value + relationType.value
});


function showForm(commId) {
    var id = "addReplyFrom+" + commId
    var form = document.getElementById(id)
    form.hidden = false
    var bntId = 'addCommButton+' + commId
    document.getElementById(bntId).hidden = true
    console.log(bntId)
}
