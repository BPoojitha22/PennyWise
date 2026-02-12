// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    // Intercept clicks on month/year selector buttons and links within standard page flow if possible.
    // However, the cleanest way is a dedicated function updatePageContent(url)

    window.updatePageContent = function (url) {
        // Show loading state if desired
        $('.app-main').css('opacity', '0.5');

        $.ajax({
            url: url,
            type: 'GET',
            headers: { "X-Requested-With": "XMLHttpRequest" },
            success: function (data) {
                // Replace content
                $('.app-main').html(data);
                $('.app-main').css('opacity', '1');
                
                // Re-initialize any JS components if needed (e.g. tooltips)
            },
            error: function () {
                alert("Failed to load content.");
                $('.app-main').css('opacity', '1');
            }
        });
    };

    // Handle Month Selector Dropdown Change
    $(document).on('change', '.month-selector-dropdown', function () {
        var form = $(this).closest('form');
        var url = form.attr('action') + '?' + form.serialize();
        // Since the form uses standard submit by default, we prevent it and use AJAX
        updatePageContent(url);
    });

    // Handle Previous/Next Month Buttons
    $(document).on('click', '.month-nav-btn', function (e) {
        e.preventDefault();
        var btn = $(this);
        var form = btn.closest('form');
        
        // Temporarily set the target year value based on logic (handled in HTML onclick already)
        // But for AJAX, we need to construct the URL manually or let the form logic run then intercept.
        
        // Simpler: The button has a value (month). The year input has a value.
        // We need to know the calculated year.
        // Let's rely on the server-side logic or improved HTML attributes.
        // Actually, the existing onclick handles updating the year input.
        // So we just need to serialize the form AFTER the click event logic has run? 
        // No, click event runs first.
        
        // Let's parse the button's intended destination.
        var month = btn.val();
        var yearInput = form.find('input[name="year"]');
        var year = yearInput.val();
        
        // The onclick in HTML updates the year input value nicely.
        // We can let that run, but we need to prevent form submit.
        // The onclick runs before the click handler if defined inline? No.
        
        // Let's reconstruct the URL manually from data attributes to be safe.
        // Or cleaner: submit the form via AJAX.
        
        setTimeout(function() {
            var url = form.attr('action') + '?' + form.serialize();
             // Manually override the month/year param with the button's clicked value/logic?
             // Since the button is type="submit", its value is sent only if clicked.
             // But serialize() typically excludes submit button values.
             
             // We need to append the month from the button.
             url += '&month=' + month;
             
             updatePageContent(url);
        }, 10); // Small delay to let inline onclick update the year input
    });
    
    // Intercept form submission
    $(document).on('submit', '.month-selector-form', function(e) {
        e.preventDefault();
        // This handles the manual enter key on year input etc.
        // We need to know which button invoked it?
        // Standard submit.
        var url = $(this).attr('action') + '?' + $(this).serialize();
        updatePageContent(url);
    });
});
