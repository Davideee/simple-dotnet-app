<template>
  <div class="user-component">
    <h1>User List</h1>
    <p>This component demonstrates fetching a list of users from the server.</p>

    <div v-if="loading" class="loading">
      Loading... Please wait.
    </div>

    <div v-if="users.length > 0" class="content">
      <table>
        <thead>
        <tr>
          <th>User</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="(user, index) in users" :key="index">
          <td>{{ user }}</td>
        </tr>
        </tbody>
      </table>
    </div>

    <div v-else>
      <p>No users found.</p>
    </div>
  </div>
</template>

<script lang="ts">
import {defineComponent, ref, onMounted} from 'vue';

// Type für die Benutzerliste
type Users = string[];

export default defineComponent({
  setup() {
    // Reaktive Variablen mit Composition API
    const loading = ref<boolean>(false);
    const users = ref<Users>([]);

    // Methode zum Abrufen der Benutzerdaten
    const fetchData = async () => {
      loading.value = true;

      try {
        const response = await fetch('/api/User'); // Der Proxy leitet dies an https://localhost:5065/api/User weiter
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        const data = await response.json();
        users.value = data;
      } catch (error) {
        console.error('Error fetching users:', error);
      } finally {
        loading.value = false;
      }
    };


    // Aufrufen der fetchData-Methode, wenn die Komponente gemountet wird
    onMounted(() => {
      fetchData();
    });

    // Rückgabe der reaktiven Variablen und Methoden
    return {
      loading,
      users
    };
  }
});
</script>

<style scoped>
th {
  font-weight: bold;
}

th, td {
  padding-left: .5rem;
  padding-right: .5rem;
}

.user-component {
  text-align: center;
}

table {
  margin-left: auto;
  margin-right: auto;
}
</style>
