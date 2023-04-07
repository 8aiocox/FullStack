<template>
  <v-container class="sms-composer">
    <v-layout>
      <div class="mx-auto">
        <v-alert v-if="showAlert" :type="alertType" style="display:flex !important; color: initial; text-align:left">
          {{alertMessage}}
        </v-alert>
        <v-card v-if="!showAlert" class="pa-3 card-container" style="min-width:500px">
          <v-form v-model="valid" ref="form">
            <v-text-field label="Mobile Number" :rules="[v => !!v || 'Item is required']" v-model="mobileNumber"></v-text-field>
            <v-textarea label="Sms Content" v-model="message" :rules="messageRules"></v-textarea>
            <v-btn class="mt-4" style="color: initial;" block @click="sendMessage">Send Message</v-btn>
          </v-form>
        </v-card>
      </div>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
  import { defineComponent } from 'vue'

  export default defineComponent({
    // type inference enabled
    data() {
      return {
        message: null,
        mobileNumber: null,
        showAlert: false,
        alertMessage: "Sent!",
        alertType: "success",
        valid: false,
        api_url: import.meta.env.VITE_API_URL,
        messageRules: [
          v => !!v || 'Message is required',
          v => (v && v.length < 160) || 'Message text must not be longer than 160 characters long']
      }
    },
    mounted() {
      // this.name // type: string | undefined
      console.log(import.meta.env.VITE_API_URL);
    },
    methods: {
      async sendMessage() {
        this.showAlert = false;
        console.log("Sending message!");
        const { valid } = await this.$refs.form.validate()
        if (!valid) {
          return;
        }
        console.log("mobileNumber", this.mobileNumber);
        console.log("message", this.message);
        fetch(this.api_url+"/sms", {
          method: "POST",
          headers: {
            // 'Accept': 'application/json',
            'Content-Type': 'application/json',
          },
          credentials: 'include',
          body: JSON.stringify({ to: this.mobileNumber, message: this.message }),
        })
          // .then((response) => response.json())
          .then((result) => {
            if (result.status >= 200 && result.status < 300) {
              this.alertType = "success";
              this.mobileNumber = "";
              this.message = "";
              console.log("Success:", result);
              this.alertMessage = "SMS sent successfully.";
              this.showAlert = true;
              setTimeout(() => {
                this.showAlert = false;
                console.log(this.show)
              }, 3000)
            } else {
              throw ("Failed!");
            }
          })
          .catch((error) => {
            this.alertMessage = "Unable to send SMS";
            this.showAlert = true;
            setTimeout(() => {
              this.showAlert = false;
              console.log(this.show)
            }, 3000)
            this.alertType = "error";
            console.error("Error:", error);
          });
      },
    }
  })
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="stylus">
</style>
