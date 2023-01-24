$(document).ready(function () {
    setHandlers();
});

function loadMore(params) {
    $.ajax({
        url: '/People/'+params.Action,
        data: params,
        success: function (data) {
            $('.show-more').remove();
            $('#peopleList').append(data);
            setHandlers();
        }
    });
}

function setHandlers() {
    $('.person-row').off('click').on('click', function (e) {
        var id = $(e.currentTarget).attr('data-id');
        showDetails(id);
    });
}

function loadRecords() {
    $('.loader-start').hide();
    $('.loader-wait').removeClass("d-none");
    $.ajax({
        url: '/Home/LoadRecords',
        success: function (data) {
            window.location.href = window.location.origin;
        }
    });
}

function showDetails(id) {
    $('#pbModalContent').html($('#modalLoading').html());
    $('#pbModal').modal('show');

    $.ajax({
        url: '/Details',
        data: { id: id },
        success: function (data) {
            $('#pbModalContent').html(data);
        }
    });
}