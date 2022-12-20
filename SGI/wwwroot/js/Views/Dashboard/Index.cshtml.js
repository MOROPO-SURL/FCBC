var varScope = $('#js-var-scope-layout');
const tracesByDayLabels = JSON.parse(varScope.attr('data-traces-by-day-labels'));
const weekStatisticsLabels = JSON.parse( varScope.attr('data-week-statistics-labels'));
const TracesbyUsers = JSON.parse(varScope.attr('data-traces-by-users'));
const WeekTracesStatisticsValues = JSON.parse(varScope.attr('data-Week-traces-statistics-values'));

const tracesByData = {
    labels: tracesByDayLabels,
    datasets: [{
        label: 'Registros por usuario',
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
        label: 'Registros por días',
        data: WeekTracesStatisticsValues,
    fill: true,
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

const dataPie = {
    labels: [
        'Creador',
        'Grupos de Creación',
        'Contratista'
    ],
    datasets: [{
        label: 'Institición',
        data: [300, 50, 100],
        backgroundColor: [
            'rgb(75, 192, 192)',
            'rgb(54, 162, 235)',
            'rgb(255, 205, 86)'
        ],
        hoverOffset: 4
    }]
};

const pieConfig = {
    type: 'pie',
    data: dataPie,
};

const pieChart = new Chart(
    document.getElementById('pieChart'),
    pieConfig
);

const dataDoughnut = {
    labels: [
        'Artes Plásticas',
        'Artesanía',
        'Diseño',
        'Servicios'
    ],
    datasets: [{
        label: '',
        data: [300, 50, 100, 10],
        backgroundColor: [
            'rgb(255, 99, 132)',
            'rgb(54, 162, 235)',
            'rgb(255, 205, 86)',
            'rgb(75, 192, 192)'
        ],
        hoverOffset: 4
    }]
};

const doughnutConfig = {
    type: 'doughnut',
    data: dataDoughnut
};

const doughnutChart = new Chart(
    document.getElementById('doughnutChart'),
    doughnutConfig
);

const dataPolarArea = {
    labels: [
        'Pintura',
        'Dibujo',
        'Collage',
        'Mural',
        'Alfarería',
        'Cerámica',
        'Orfebrería',
        'Herrería',
    ],
    datasets: [{
        label: 'My First Dataset',
        data: [28, 16, 25, 20, 14, 19, 30, 34],
        backgroundColor: [
            '#4dc9f6',
            '#f67019',
            '#f53794',
            '#537bc4',
            '#acc236',
            '#166a8f',
            '#00a950',
            '#58595b'
        ]
    }]
};

const polarAreaConfig = {
    type: 'polarArea',
    data: dataPolarArea,
    options: {}
};

const polarAreaChart = new Chart(
    document.getElementById('polarAreaChart'),
    polarAreaConfig
);
