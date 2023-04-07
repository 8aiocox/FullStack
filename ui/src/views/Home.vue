<template>
  <div class="d-flex">
    <div class="sms-list mx-auto">
      <div class="my-3">
        <h6>Filter By Date Time</h6>
        <VueDatePicker v-model="date" @update:model-value="getMessages"></VueDatePicker>
      </div>
      <v-layout class="" v-for="item in messages">
        <!-- :key="item.account_sid"-->
        <v-card class="card-container mx-auto my-3 pa-2">
          <div class="text-sm-left">{{ item.body }}</div>
          <v-flex class="mt-2 d-flex">
            <v-sheet class="ma-2 pa-2">{{ item.Status }}</v-sheet>
            <v-sheet class="ma-2 pa-2">{{ item.to }}</v-sheet>
            <v-sheet class="ma-2 pa-2">{{ format(item.dateSent)   }}</v-sheet>
          </v-flex>
        </v-card>
      </v-layout>
    </div>
  </div>
</template>
<!--setup-->
<script lang="ts">
  import { defineComponent } from 'vue'
  import moment from 'moment'

  export default defineComponent({
    // type inference enabled
    data() {
      return {
        count: 1,
        messages: [],
        api_url: import.meta.env.VITE_API_URL,
        date: null
      }
    },
    mounted() {
      // this.name // type: string | undefined
      this.getMessages();
    },
    methods: {
      format(value: any) {
        console.log(moment(value));
        return moment(value).format("dddd, DD MMMM YYYY HH:mm");
      },
      async getMessages() {
        let sent_date = this.date;
        console.log("typeof: ", typeof sent_date);
        console.log(this.date);
        if (sent_date) {
          let r = sent_date.toISOString().split(".000Z");
          sent_date = r[0];
        }
        let url = this.api_url+"/sms";
        url += "?date_sent=" + sent_date;
        fetch(url, {
          headers: {
            'Access-Control-Allow-Origin': '*'
          },
          method: "GET",
          credentials: 'include',
        })
          .then((response) => response.json())
          .then((result) => {
            console.log("Results: ", result);
            this.messages = result;
          })
          .catch((error) => {
            console.error("Error:", error);
          });
      },
    }
  })
</script>
