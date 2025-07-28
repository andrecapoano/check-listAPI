const apiUrl = 'https://localhost:7028/api/tasks';
const token = localStorage.getItem('token');
const userId = localStorage.getItem('userId');
const taskList = document.getElementById('taskList');
const messageDiv = document.getElementById('message');
const taskFormContainer = document.getElementById('taskFormContainer');
const btnNewTask = document.getElementById('btnNewTask');

if (!token || !userId) {
  alert("Você precisa estar logado.");
  window.location.href = "index.html";
}

window.onload = () => {
  const username = localStorage.getItem('username');
  const welcomeDiv = document.getElementById('welcome-message');
  if (username) {
    welcomeDiv.textContent = `Bem-vindo, ${username}!`;
  }

  taskFormContainer.style.display = 'none';
  loadTasks();
};


btnNewTask.onclick = () => {
  taskFormContainer.style.display = taskFormContainer.style.display === 'none' ? 'flex' : 'none';
};

function formatDate(dateString) {
  const date = new Date(dateString);
  const day = String(date.getDate()).padStart(2, '0');
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const year = date.getFullYear();
  const hours = String(date.getHours()).padStart(2, '0');
  const minutes = String(date.getMinutes()).padStart(2, '0');
  return `${day}/${month}/${year} ${hours}:${minutes}`;
}

async function loadTasks() {
  try {
    const response = await fetch(apiUrl, {
      headers: { 'Authorization': 'Bearer ' + token }
    });

    if (!response.ok) throw new Error("Erro ao carregar tarefas");

    const tasks = await response.json();
    taskList.innerHTML = '';

    tasks.forEach(task => {
      const li = document.createElement('li');
      li.classList.add('task-item');
      if (task.isCompleted) li.classList.add('completed');

      const formattedDate = formatDate(task.createdAt);

      li.innerHTML = `
        <div class="task-header">
          <span class="task-date">${formattedDate}</span>
          <div class="task-actions">
            <button onclick="enableEditing(this)">Editar</button>
            <button onclick="deleteTask(${task.id})">Excluir</button>
          </div>
        </div>

        <div class="task-content">
          <strong>${task.title}</strong>
          <p class="description">${task.description || ''}</p>

          <input type="text" class="edit-title" value="${task.title}" disabled />
          <input type="text" class="edit-description" value="${task.description || ''}" disabled />

          <div class="edit-actions">
            <button onclick="saveTask(${task.id}, this)">Salvar</button>
            <button onclick="cancelEdit(this)">Cancelar</button>
          </div>

          <button class="toggle-btn" onclick="toggleStatus(${task.id}, ${task.isCompleted})">
            ${task.isCompleted ? 'Tarefa concluída.' : 'Concluir tarefa.'}
          </button>
        </div>
      `;

      taskList.appendChild(li);
    });
  } catch (error) {
    messageDiv.textContent = error.message;
  }
}

async function addTask() {
  const titleInput = document.getElementById('taskTitle');
  const descriptionInput = document.getElementById('taskDescription');
  const title = titleInput.value.trim();
  const description = descriptionInput.value.trim();

  if (!title || !description) {
    messageDiv.textContent = "Preencha título e descrição.";
    messageDiv.style.color = 'red';
    return;
  }

  const body = {
    title,
    description,
    userId: Number(userId)
  };

  try {
    const response = await fetch(apiUrl, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      },
      body: JSON.stringify(body)
    });

    if (!response.ok) throw new Error("Erro ao adicionar tarefa");

    titleInput.value = '';
    descriptionInput.value = '';
    taskFormContainer.style.display = 'none';
    loadTasks();
  } catch (error) {
    messageDiv.textContent = error.message;
  }
}

function enableEditing(button) {
  const taskContent = button.closest('li').querySelector('.task-content');
  taskContent.classList.add('editing');

  const titleInput = taskContent.querySelector('.edit-title');
  const descInput = taskContent.querySelector('.edit-description');

  titleInput.disabled = false;
  descInput.disabled = false;

  taskContent.querySelector('.edit-actions').style.display = 'flex';
}

function cancelEdit(button) {
  const taskContent = button.closest('.task-content');
  taskContent.classList.remove('editing');

  const titleInput = taskContent.querySelector('.edit-title');
  const descInput = taskContent.querySelector('.edit-description');

  titleInput.disabled = true;
  descInput.disabled = true;

  taskContent.querySelector('.edit-actions').style.display = 'none';
}

async function saveTask(id, button) {
  const taskContent = button.closest('.task-content');
  const title = taskContent.querySelector('.edit-title').value.trim();
  const description = taskContent.querySelector('.edit-description').value.trim();

  if (!title || !description) {
    messageDiv.textContent = "Título e descrição obrigatórios.";
    return;
  }

  const body = {
    id,
    title,
    description,
    isCompleted: taskContent.closest('li').classList.contains('completed'),
    userId: Number(userId)
  };

  try {
    const response = await fetch(`${apiUrl}/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      },
      body: JSON.stringify(body)
    });

    if (!response.ok) throw new Error("Erro ao atualizar tarefa");

    loadTasks();
  } catch (error) {
    messageDiv.textContent = error.message;
  }
}

async function deleteTask(id) {
  try {
    const response = await fetch(`${apiUrl}/${id}`, {
      method: 'DELETE',
      headers: { 'Authorization': 'Bearer ' + token }
    });

    if (!response.ok) throw new Error("Erro ao excluir tarefa");

    loadTasks();
  } catch (error) {
    messageDiv.textContent = error.message;
  }
}

async function toggleStatus(id, isCompleted) {
  try {
    const li = [...taskList.children].find(li => li.innerHTML.includes(`toggleStatus(${id},`));
    const title = li.querySelector('.edit-title').value;
    const description = li.querySelector('.edit-description').value;

    const body = {
      id,
      title,
      description,
      isCompleted: !isCompleted,
      userId: Number(userId)
    };

    const response = await fetch(`${apiUrl}/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      },
      body: JSON.stringify(body)
    });

    if (!response.ok) throw new Error("Erro ao alterar status da tarefa");

    loadTasks();
  } catch (error) {
    messageDiv.textContent = error.message;
  }
}

function logout() {
  localStorage.removeItem('token');
  localStorage.removeItem('userId');
  window.location.href = "index.html";
}