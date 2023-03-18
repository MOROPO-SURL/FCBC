var varScope = $('#js-var-scope-layout');
const tracesByDayLabels = JSON.parse(varScope.attr('data-traces-by-day-labels'));
const weekStatisticsLabels = JSON.parse(varScope.attr('data-week-statistics-labels'));
const TracesbyUsers = JSON.parse(varScope.attr('data-traces-by-users'));
const WeekTracesStatisticsValues = JSON.parse(varScope.attr('data-Week-traces-statistics-values'));
const CreatorCount = JSON.parse(varScope.attr('data-Creator-Count'));
const CreationGroupCount = JSON.parse(varScope.attr('data-Creation-Group-Count'));
const ContractorCount = JSON.parse(varScope.attr('data-Contractor-Count'));
const ArtWorkCount = JSON.parse(varScope.attr('data-Art-Work-Count'));
const CraftWorkCount = JSON.parse(varScope.attr('data-Craft-Work-Count'));
const DesignWorkCount = JSON.parse(varScope.attr('data-Design-Work-Count'));
const ServiceWorkCount = JSON.parse(varScope.attr('data-Service-Work-Count'));
const manifestationsNames = JSON.parse(varScope.attr('data-Manifestations-Names'));
const ManifestationsCounts = JSON.parse(varScope.attr('data-Manifestations-Counts'));

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
        data: [CreatorCount, CreationGroupCount, ContractorCount],
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
        data: [ArtWorkCount, CraftWorkCount, DesignWorkCount, ServiceWorkCount],
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
    labels: manifestationsNames,
    datasets: [{
        data: ManifestationsCounts,
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
