function showOptions(userInput, optionsListt, users, userIdInput) {
    const input = document.getElementById(userInput);
    const inputValue = input.value.toLowerCase();


    const optionsList = document.getElementById(optionsListt);
    optionsList.innerHTML = '';

    var userList = [];

    users.forEach(u => {
        var fullName = u.Name + " " + u.LastName + " (" + u.Id + ")";
        userList.push(fullName);
    });

    const filteredOptions = userList.filter(option =>
        option.toLowerCase().includes(inputValue)
    );


    filteredOptions.forEach((option, index) => {
        const optionItem = document.createElement('div');
        optionItem.textContent = option.split('(')[0];
        optionItem.classList.add('option');

        optionItem.addEventListener('click', () => {
            input.value = option.split('(')[0];
            optionsList.style.border = '';
            optionsList.innerHTML = '';

            
            const idInputElem = document.getElementById(userIdInput);
            if (idInputElem) {
                idInputElem.value = filteredOptions[index].match(/\((.*?)\)/)?.[1];
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
    input.addEventListener('input', showOptions);

}
