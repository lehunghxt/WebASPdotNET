$(document).ready(function () {
    $('[data-sale-countdown]').each(function () {
        var $this = $(this),
            finalDate = $(this).data('sale-countdown');
        $this.countdown(finalDate, function (event) {
            $this.html(event.strftime(`<span data-value="%D" class="days">
                                            <span class="value">%D</span>
                                        </span> ngày
                                        <span class="hours">
                                            <span class="value">%H</span>
                                        </span> :
                                        <span class="minutes">
                                            <span class="value">%M</span>
                                        </span> : 
                                        <span class="seconds">
                                            <span class="value">%S</span>
                                        </span>
                                    `));
        });
    });
    $('.desktop-only .departments-menu button.dropdown-toggle').on('click', function (e) {
        e.preventDefault();
        //var $this = $(this);
        $('#departments-menu').toggleClass('show');
    });

});
