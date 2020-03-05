new Vue({
    el: '#app',
    data: {
        hosts: [],
        message: ''
    },
    created() {
        this.loadHosts();
    },
    methods: {
        async loadHosts() {
            const res = await axios.get('/api/wol/hosts')
            this.hosts = res.data
        },
        async wakeHost(host) {
            try {
                await axios.post(`/api/wol/hosts/${host}`)
                this.sendMessage('wake request already sand.')
            } catch (error) {
                this.sendMessage(error.message)
                console.error(error)
            }
        },
        sendMessage(message) {
            this.message = message
            setTimeout(() => {
                this.message = ''
            }, 3000)
        }
    }
})
