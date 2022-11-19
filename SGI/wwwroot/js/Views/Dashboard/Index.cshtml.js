var varScope = $('#js-var-scope-layout');
const tracesByDayLabels = JSON.parse(varScope.attr('data-traces-by-day-labels'));
const weekStatisticsLabels = JSON.parse( varScope.attr('data-week-statistics-labels'));
const TracesbyUsers = JSON.parse(varScope.attr('data-traces-by-users'));
const WeekTracesStatisticsValues = JSON.parse(varScope.attr('data-Week-traces-statistics-values'));

const tracesByData = {
    labels: tracesByDayLabels,
    datasets: [{
        label: 'Cantidad de registros por usuario',
        data: TracesbyUsers,
backgroundColor: [
    'rgba(255, 99, 132, 0.2)',
    'rgba(255, 159, 64, 0.2)',
    'rgba(255, 205, 86, 0.2)',
    'rgba(75, 192, 192, 0.2)',
    'rgba(54, 162, 235, 0.2)',
    'rgba(153, 102, 255, 0.2)',
    'rgba(201, 203, 207, 0.2)'
],
    borderColor: [
        'rgb(255, 99, 132)',
        'rgb(255, 159, 64)',
        'rgb(255, 205, 86)',
        'rgb(75, 192, 192)',
        'rgb(54, 162, 235)',
        'rgb(153, 102, 255)',
        'rgb(201, 203, 207)'
    ],
        borderWidth: 1
            }]
        };

const tracesByConfig = {
    type: 'bar',
    data: tracesByData,
    options: {}
};

const tracesByUsersChart = new Chart(
    document.getElementById('tracesByUsersChart'),
    tracesByConfig
);

const weekStatisticsData = {
    labels: weekStatisticsLabels,
    datasets: [{
        label: 'Cantidad de registros por días',
        data: WeekTracesStatisticsValues,
fill: false,
    borderColor: 'rgb(75, 192, 192)',
        backgroundColor: 'rgba(75, 192, 192, 0.5)',
            tension: 0.1
            }]
        };

const weekStatisticsConfig = {
    type: 'line',
    data: weekStatisticsData,
    options: {}
};

const weekStatisticsChart = new Chart(
    document.getElementById('weekStatisticsChart'),
    weekStatisticsConfig
);
