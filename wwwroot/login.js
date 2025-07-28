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
      const data = await response.json();
  
      if (!data.token) {
        messageDiv.style.color = 'red';
        messageDiv.textContent = 'Token não recebido. Login falhou.';
        return;
      }
      
      localStorage.setItem('token', data.token);
      localStorage.setItem('userId', data.user.id);
      localStorage.setItem('username', data.user.username);

      messageDiv.style.color = 'green';
      messageDiv.textContent = `Bem-vindo, ${data.user.username}!`;

      setTimeout(() => {
        window.location.href = 'dashboard.html';
      });

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
