function showDateTime() {
    var now = new Date();
    var dateString = now.toLocaleDateString('pl-PL') + ' ' + now.toLocaleTimeString('pl-PL');
    var footerElement = document.getElementById('currentDateTime');
    if (footerElement) {
        footerElement.textContent = 'Movie Rental System: ' + dateString;
    }
}

function countItems(itemType) {
    var rows = document.querySelectorAll('table tbody tr');
    var count = rows.length;
    var heading = document.querySelector('h1');
    if (heading && count > 0) {
        heading.textContent = heading.textContent + ' (' + count + ')';
    }
}

function setupSearch(searchInputId, tableId) {//działa, ale dla Rentals się resetuje filtr przy keyup
    var searchInput = document.getElementById(searchInputId);
    if (!searchInput) return;

    searchInput.addEventListener('keyup', function() {
        var searchText = this.value.toLowerCase();
        var tableRows = document.querySelectorAll('#' + tableId + ' tbody tr');

        tableRows.forEach(function(row) {
            var rowText = row.textContent.toLowerCase();
            if (rowText.includes(searchText)) {
                row.classList.remove('d-none');
            } else {
                row.classList.add('d-none');//display none
            }
        });
    });
}

document.addEventListener('DOMContentLoaded', function() {
    console.log('Video Rental JavaScript loaded!');

    showDateTime();
    setInterval(showDateTime, 1000);
});