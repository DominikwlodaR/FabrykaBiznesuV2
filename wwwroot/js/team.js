function showOptions(userInput, optionsListId, users, teamId, bnt) {
    const input = document.getElementById(userInput);
    const inputValue = input.value.toLowerCase();
   
    const optionsList = document.getElementById(optionsListId);
    optionsList.innerHTML = '';



    const userList = users.map(u => u.Name + ' ' + u.LastName);

    const filteredOptions = userList.filter(option =>
        option.toLowerCase().includes(inputValue)
    );

    filteredOptions.forEach(option => {
        const optionItem = document.createElement('div');
        optionItem.textContent = option;
        optionItem.classList.add('option');

        optionItem.addEventListener('click', () => {
            input.value = option;
            optionsList.style.border = '';
            optionsList.innerHTML = '';

            const idInputElem = document.getElementById(teamId);
            if (idInputElem) {
                const selectedUser = users.find(u => u.Name + ' ' + u.LastName === option);
                idInputElem.value = selectedUser ? selectedUser.Id : '';
            }
        });

        optionsList.appendChild(optionItem);
    });

    document.addEventListener('click', function (event) {
        const isClickInsideInput = input.contains(event.target);
        const isClickInsideOptionsList = optionsList.contains(event.target);

        if (!isClickInsideInput && !isClickInsideOptionsList) {
            optionsList.innerHTML = '';
        }
    });

    input.addEventListener('input', () => showOptions(userInput, optionsListId, users, teamId));
 

}

function validate(taskId, inputId, teams, alert) {
    var teamId = document.getElementById(inputId);
    if (!teamId.value) {
        document.getElementById(alert).hidden = false;
    }
    else {
        document.getElementById(alert).hidden = true;
        var Data = {
            Id: taskId,
            UserId: teamId.value
        }
        console.log(Data.Id)
        $.ajax({
            type: 'POST',
            url: '/Team/AssignUser',
            data: Data,
            success: function (data) {
                location.reload();

            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("Request failed: " + textStatus, errorThrown);
                console.log(xhr.responseText);
            }
        });
    }
}

function EditTeamAssign(taskId, teamID) {

    var inputName = "selectTeam_" + taskId
    var bntName = "saveTeamBtn_" + taskId

    var input = document.getElementById(inputName)

    var bnt = document.getElementById(bntName)

    if (input.disabled) {
        input.disabled = false;
        bnt.hidden = false;
    }


}


