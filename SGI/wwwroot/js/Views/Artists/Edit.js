var varScope = $('#js-var-scope-layout');
const dataSelectOptionArt = JSON.parse(varScope.attr('data-select-option-artisc'));

const options = [];

dataSelectOptionArt.forEach(function (data) {
    options.push({ label: data.Text, title: data.Text, value: data.Value, selected: data.Selected });
});

$('.listbox').multiselect('dataprovider', options, {
    includeSelectAllOption: true,
    templates: {
        li: '<li><a href="javascript:void(0);"><label class="pl-2"></label></a></li>'
    }
});