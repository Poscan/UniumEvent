<template>
  <div>
    <transition name="authAnim">
      <div class="authorize-page" v-if="isAuthorize" key="authorize">
        <div class="aleft">
          <div class="authorize-form">
            <h1>Войти в систему</h1>
            <label class="custom-field one">
              <input type="text" placeholder=" " v-model="login" />
              <span class="placeholder">Введите почту</span>
            </label>

            <label class="custom-field one">
              <input type="password" placeholder=" " v-model="password" />
              <span class="placeholder">Введите Пароль</span>
            </label>

            <label class="registr" style="color: red" v-if="error.length > 0">{{ error }}</label>

            <button class="submit" :disabled="isNextStep" @click="authorize">Продолжить</button>
            <label class="registr">У вас нет аккаунта? <a @click="isAuthorize = false" class="anotherPage">Зарегистрируйтесь</a></label>
          </div>
        </div>
        <div class="authorize-back">
          <img src="Authorization.svg" width="800px" />
        </div>
      </div>
      <div class="authorize-page" v-else key="registr">
        <div class="authorize-back">
          <img src="Registration.svg" height="740px" />
        </div>
        <div class="aleft">
          <div class="authorize-form">
            <h1>Зарегистрироваться</h1>
            <label class="custom-field one">
              <input type="text" placeholder=" " v-model="lastName" />
              <span class="placeholder">Введите фамилию</span>
            </label>

            <label class="custom-field one">
              <input type="text" placeholder=" " v-model="firstName" />
              <span class="placeholder">Введите имя</span>
            </label>

            <label class="custom-field one">
              <input type="text" placeholder=" " v-model="patrName" />
              <span class="placeholder">Введите отчество</span>
            </label>

            <label class="custom-field one">
              <input type="text" placeholder=" " v-model="login" />
              <span class="placeholder">Введите почту</span>
            </label>

            <label class="custom-field one">
              <input type="password" placeholder=" " v-model="password" />
              <span class="placeholder">Введите Пароль</span>
            </label>

            <label class="registr" style="color: red" v-if="error.length > 0">{{ error }}</label>

            <button class="submit" :disabled="isNextStep" @click="authorize">Продолжить</button>
            <label class="registr">У вас есть аккаунт? <a @click="isAuthorize = true" class="anotherPage">Войти</a></label>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import AuthService from "../services/AuthService";

export default Vue.extend({
  data() {
    return {
      login: "",
      password: "",
      isAuthorize: true,
      lastName: "",
      firstName: "",
      patrName: "",
      error: "",
    };
  },

  computed: {
    isNextStep(): boolean {
      var result = this.login.length > 0 && this.password.length > 0;

      if (!this.isAuthorize) {
        result = result && this.lastName.length > 0 && this.firstName.length > 0;
      }

      return !result;
    },
  },

  methods: {
    async authorize() {
      var result = null;
      if (this.isAuthorize) {
        result = (await AuthService.login(this.login, this.password)) as any;

        this.error = result.data.isSuccessful ? "" : result.data.Errors[0].Message;
      } else {
        result = (await AuthService.createAccount(this.login, this.password)) as any;
        this.$store.state.user.lastName = this.lastName;
        this.$store.state.user.firstName = this.firstName;
        this.$store.state.user.patrName = this.patrName;

        this.error = result.data.isSuccessful ? "" : result.data.errors[0].message;
      }

      if (result.data.isSuccessful) {
        localStorage.setItem("accessToken", result.data.data.accessToken);
        localStorage.setItem("refreshToken", result.data.data.refreshToken);
      }
    },
  },

  watch: {
    isAuthorize() {
      this.error = "";
    },
  },

  mounted() {
    console.log(this.$route.name);
  },
});
</script>

<style lang="scss">
.authorize-page {
  height: 100vh;
  position: absolute;
  top: 0;
  left: 0;
  z-index: -999;
  display: flex;
  align-items: center;
  width: 100vw;
}

.aleft {
  display: flex;
  align-content: center;
  justify-content: center;
  background: white;
  width: 800px;
  height: 100vh;
}

.authorize-form {
  display: flex;
  flex-direction: column;
  align-items: stretch;
  padding: 200px;
  justify-content: center;
  width: 400px;
}

.authorize-back {
  width: calc(100% - 800px);
  background: rgb(245, 128, 122);
  background: linear-gradient(330deg, rgba(245, 128, 122, 1) 0%, rgba(198, 42, 114, 1) 0%, rgba(30, 100, 130, 1) 100%);
  height: 100%;

  display: flex;
  justify-content: center;
  align-items: center;
}

.anotherPage {
  color: #6eb6f5;

  &:hover {
    text-decoration: underline;
    cursor: pointer;
  }
}

.submit {
  margin-top: 20px;
  margin-bottom: 5px;

  padding: 12px;

  background: #6eb6f5;
  color: white;

  font-size: 16px;
  border-radius: 3px;
  border: 2px solid transparent;

  &:disabled {
    opacity: 60%;
  }

  &:hover:enabled {
    background: #5aa5ef;
    cursor: pointer;
  }
}

.registr {
  margin-top: 20px;
  margin-bottom: 5px;
}

/*_______________________________________________________________________*/
/*Animation authorization form*/
.authAnim-enter-active {
  transition: all 0.5s ease;
}
.authAnim-leave-active {
  transition: all 0.5s ease; /*cubic-bezier(1, 0.5, 0.8, 1);*/
}
.authAnim-enter {
  transform: translateX(50vh);
  opacity: 0;
}
.authAnim-leave-to {
  transform: translateX(50vh);
  opacity: 100;
}

/*_______________________________________________________________________ */

.custom-field {
  position: relative;
  font-size: 16px;
  border-top: 20px solid transparent;
  margin-bottom: 5px;
  --field-padding: 12px;
}

.custom-field input {
  border: none;
  -webkit-appearance: none;
  -ms-appearance: none;
  -moz-appearance: none;
  appearance: none;
  background: #f2f2f2;
  padding: var(--field-padding);
  border-radius: 3px;
  width: calc(100% - (var(--field-padding) * 2) - 4px);
  outline: none;
  font-size: 16px;
}

.custom-field .placeholder {
  position: absolute;
  left: var(--field-padding);
  width: calc(100% - (var(--field-padding) * 2) - 4px);
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
  top: 22px;
  line-height: 100%;
  transform: translateY(-50%);
  color: #aaa;
  transition: top 0.3s ease, color 0.3s ease, font-size 0.3s ease;
}

.custom-field input.dirty + .placeholder,
.custom-field input:focus + .placeholder,
.custom-field input:not(:placeholder-shown) + .placeholder {
  top: -10px;
  font-size: 16px;
  color: #222;
}

.custom-field.one input {
  background: none;
  border: 2px solid #ddd;
  transition: border-color 0.3s ease;
}

.custom-field.one input + .placeholder {
  left: 8px;
  padding: 0 5px;
}

.custom-field.one input.dirty,
.custom-field.one input:not(:placeholder-shown),
.custom-field.one input:focus {
  border-color: #6eb6f5;
  transition-delay: 0.1s;
}

.custom-field.one input.dirty + .placeholder,
.custom-field.one input:not(:placeholder-shown) + .placeholder,
.custom-field.one input:focus + .placeholder {
  top: 0;
  font-size: 14px;
  color: #222;
  background: #fff;
  width: auto;
}
</style>
