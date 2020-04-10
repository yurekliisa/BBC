<template>
  <v-layout>
    <v-flex justify-center>
      <v-card
      class="mx-auto "
      max-width="500"
      raised          
      >
      <form>
        <v-text-field
          v-model="email"
        
          label="E-mail"
          required
      
        >   
        </v-text-field><!--  @input="$v.email?.$touch()"
        @blur="$v.email?.$touch()" :error-messages="emailErrors"-->
        
        <v-text-field
          v-model = "password"
          :append-icon = "show? 'mdi-eye':'mdi-eye-off'"
          :rules = "[rules.required, rules.min]"
          :type = "show? 'text' : 'password' "
          name= "input-10-1"
          hint= "At least 8 chracters"
          counter
          label = "Password"
           @click:append= " show = !show"        
        >   
        </v-text-field>
        <!--  @input="$v.name?.$touch()"
         @blur="$v.name?.$touch()" :error-messages="passwordErrors"-->
          <v-checkbox
            v-model="checkbox"
            label="Remember me"
            required
     
          > 
          </v-checkbox><!-- @change="$v.checkbox?.$touch()"
          @blur="$v.checkbox?.$touch()" :error-messages="checkboxErrors"-->
      </form>  
      <v-card-actions>
        <v-btn text>Submit</v-btn>
        
      </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<style>
.login{
  height: 1000px;
  
}

</style>
<script>
  import { validationMixin } from 'vuelidate'
  //import { required, maxLength, email } from 'vuelidate/lib/validators'

  export default {
   // mixins: [validationMixin],

    /*validations: {
      password: { required, maxLength: maxLength(8) },
      email: { required, email },
      checkbox: {
        checked (val) {
          return val
        },
      },
    },*/

    data() { 
    return {
      show: false,
      password: '',
      rules:{
        requried: value =>!!value || 'Required',
        min: v => v.lenght >= 8 || 'Min 8 charecters',
      },
      email: '',
      checkbox: false,
    }
    },

   /* computed: {
      checkboxErrors () {
        const errors = []
        if (!this.$v.checkbox?.$dirty) return errors
        !this.$v.checkbox.checked && errors.push('You must agree to continue!')
        return errors
      },      
      emailErrors () {
        const errors = []
        if (!this.$v.email?.$dirty) return errors
        !this.$v.email.email && errors.push('Must be valid e-mail')
        !this.$v.email.required && errors.push('E-mail is required')
        return errors
      },
      passwordErrors () {
        const errors = []
        if (!this.$v.name?.$dirty) return errors
        !this.$v.password.maxLength && errors.push('Password must be at most 8 characters long')
        !this.$v.password.required && errors.push('Password is required.')
        return errors
      },
    },*/

    methods: {
      submit () {
        this.$v.$touch()
      }      
    },
  }
</script>