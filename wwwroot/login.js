document.getElementById('loginForm').addEventListener('submit', async function(event) {
  event.preventDefault();

  const username = document.getElementById('username').value.trim();
  const password = document.getElementById('password').value;

  const messageDiv = document.getElementById('message');
  messageDiv.textContent = '';

  try {
    const response = await fetch('https://localhost:7028/api/user/login', {

      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ username, password })
    });

    if (response.ok) {
      const user = await response.json();
      messageDiv.style.color = 'green';
      messageDiv.textContent = `Bem-vindo, ${user.nome || user.username}!`;
      
    } else if (response.status === 400 || response.status === 401) {
      const errorText = await response.text();
      messageDiv.style.color = 'red';
      messageDiv.textContent = errorText;
    } else {
      messageDiv.style.color = 'red';
      messageDiv.textContent = 'Erro inesperado. Tente novamente.';
    }
  } catch (error) {
    messageDiv.style.color = 'red';
    messageDiv.textContent = 'Não foi possível conectar ao servidor.';
    console.error('Erro:', error);
  }
});
